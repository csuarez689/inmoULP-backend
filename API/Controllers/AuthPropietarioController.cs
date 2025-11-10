using InmobiliariaAPI.API.Utils;
using InmobiliariaAPI.Application.DTOs.AuthPropietario;
using InmobiliariaAPI.Application.Utils;
using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InmobiliariaAPI.API.Controllers;

[ApiController]
[Route("api/propietarios")]
public class AuthPropietarioController : ControllerBase
{
    private readonly ILogger<AuthPropietarioController> _logger;
    private readonly IPropietarioRepository _propietarioRepository;
    private readonly IConfiguration _configuration;
    private readonly IUserContextService _userContextService;

    public AuthPropietarioController(
        ILogger<AuthPropietarioController> logger,
        IPropietarioRepository propietarioRepository,
        IConfiguration configuration,
        IUserContextService userContextService)
    {
        _logger = logger;
        _propietarioRepository = propietarioRepository;
        _configuration = configuration;
        _userContextService = userContextService;
    }

    /// <summary>
    /// LOGIN - Autenticar propietario
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            // Buscar propietario por email
            var propietario = await _propietarioRepository.GetByEmail(request.Email, true);
            if (propietario == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var salt = _configuration["Salt"];
            var hash = PasswordHasher.HashPassword(request.Password, salt);

            if (!string.Equals(hash, propietario.password))
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            // Generar token JWT
            var token = GenerateJwtToken(propietario);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en login de propietario");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtener perfil del propietario autenticado
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(PropietarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe()
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var propietario = await _propietarioRepository.GetById(userId, true);
            
            if (propietario == null)
                return NotFound(new { message = "Propietario no encontrado" });

            var dto = new PropietarioDto
            {
                dni = propietario.dni,
                nombre = propietario.nombre,
                apellido = propietario.apellido,
                email = propietario.email,
                telefono = propietario.telefono,
            };

            return Ok(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener perfil");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    /// <summary>
    /// Actualizar perfil del propietario autenticado
    /// </summary>
    [HttpPut("me")]
    [Authorize]
    [ProducesResponseType(typeof(PropietarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdatePropietarioDto dto)
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var propietario = await _propietarioRepository.GetById(userId, true);
            
            if (propietario == null)
                return NotFound(new { message = "Propietario no encontrado" });

            var email = dto.email.Trim();

            if (!email.Equals(propietario.email, StringComparison.OrdinalIgnoreCase))
            {
                var emailEnUso = await _propietarioRepository.GetByEmail(email);
                if (emailEnUso != null && emailEnUso.id != propietario.id)
                {
                    return BadRequest(new { message = "El email ya se encuentra registrado por otro propietario" });
                }

                propietario.email = email;
            }

            (propietario.nombre, propietario.apellido, propietario.telefono) = (
                dto.nombre.Trim(),
                dto.apellido.Trim(),
                dto.telefono.Trim()
            );

            await _propietarioRepository.Update(propietario);

            var result = new PropietarioDto
            {
                dni = propietario.dni,
                nombre = propietario.nombre,
                apellido = propietario.apellido,
                email = propietario.email,
                telefono = propietario.telefono,
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar perfil");
            return BadRequest(new { message = "Error al actualizar perfil", error = ex.Message });
        }
    }

    /// <summary>
    /// Cambiar contraseña del propietario autenticado
    /// </summary>
    [HttpPut("me/changepassword")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var propietario = await _propietarioRepository.GetById(userId);
            
            if (propietario == null)
                return NotFound(new { message = "Propietario no encontrado" });

            var currentPassword = dto.CurrentPassword.Trim();
            var newPassword = dto.NewPassword.Trim();
            var confirmPassword = dto.ConfirmPassword.Trim();

            // Verificar contraseña actual
            var salt = _configuration["Salt"];
            var isValid = PasswordHasher.VerifyPassword(
                currentPassword,
                propietario.password,
                salt
            );

            if (!isValid)
                return BadRequest(new { message = "Contraseña actual incorrecta" });

            if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
                return BadRequest(new { message = "La confirmación no coincide con la nueva contraseña" });

            if (string.Equals(currentPassword, newPassword, StringComparison.Ordinal))
                return BadRequest(new { message = "La nueva contraseña debe ser distinta a la actual" });

            // Hashear nueva contraseña
            propietario.password = PasswordHasher.HashPassword(newPassword, salt);
            await _propietarioRepository.Update(propietario);

            return Ok(new { message = "Contraseña actualizada exitosamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar contraseña");
            return BadRequest(new { message = "Error al cambiar contraseña", error = ex.Message });
        }
    }

    // Método auxiliar para generar JWT
    private string GenerateJwtToken(Propietario propietario)
    {
        var secret = _configuration["Jwt:Secret"];
        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new InvalidOperationException("Falta configurar Jwt:Secret");
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expirationHours = _configuration.GetValue("Jwt:ExpirationHours", 8);

        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, propietario.id.ToString()),
            new Claim(ClaimTypes.Name, propietario.email),
            new Claim(ClaimTypes.Email, propietario.email),
            new Claim("FullName", $"{propietario.nombre} {propietario.apellido}".Trim()),
            new Claim(ClaimTypes.Role, "Propietario")
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expirationHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}