using InmobiliariaAPI.Application.DTOs.AuthPropietario;
using InmobiliariaAPI.Application.Utils;
using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InmobiliariaAPI.API.Filters; 

namespace InmobiliariaAPI.API.Controllers;

[ApiController]
[Route("api/propietarios")]
public class AuthPropietarioController : ControllerBase
{
    private readonly IPropietarioRepository _propietarioRepository;
    private readonly IConfiguration _configuration;

    public AuthPropietarioController(
        IPropietarioRepository propietarioRepository, 
        IConfiguration configuration)
    {
        _propietarioRepository = propietarioRepository;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var propietario = await _propietarioRepository.GetByEmail(request.Email, true);
        if (propietario == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        var isValid = PasswordHasher.VerifyPassword(
            request.Password,
            propietario.password,
            _configuration["Salt"]!);

        if (!isValid)
            return Unauthorized(new { message = "Credenciales inválidas" });
        return Ok(new { token = GenerateJwtToken(propietario) });
    }

    [HttpGet("me")]
    [Authorize]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public ActionResult<PropietarioDto> GetMe()
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        return PropietarioDto.FromEntity(propietario);
    }

    [HttpPut("me")]
    [Authorize]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<PropietarioDto>> UpdateMe([FromBody] UpdatePropietarioDto dto)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        
        // Validación de cambios en DNI
        if (!dto.dni.Equals(propietario.dni))
        {
            if (await _propietarioRepository.DniExists(dto.dni, propietario.id))
                return BadRequest(new { 
                    Status = 400,
                    Title = "Error de validación",
                    Errors = new { dni = new [] { "DNI ya registrado" } }
                });
            
            propietario.dni = dto.dni;
        }

        // Validación de cambios en Email
        if (!dto.email.Equals(propietario.email, StringComparison.OrdinalIgnoreCase))
        {
            if (await _propietarioRepository.EmailExists(dto.email, propietario.id))
                return BadRequest(new { 
                    Status = 400,
                    Title = "Error de validación",
                    Errors = new { email = new [] { "Email ya registrado" } }
                });
            
            propietario.email = dto.email;
        }

        // Actualizar campos
        propietario.nombre = dto.nombre;
        propietario.apellido = dto.apellido;
        propietario.telefono = dto.telefono;

        await _propietarioRepository.Update(propietario);
        return Ok(PropietarioDto.FromEntity(propietario));
    }

    [HttpPut("me/changepassword")]
    [Authorize]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        
        // Validar contraseña actual
        var isValid = PasswordHasher.VerifyPassword(
            dto.CurrentPassword,
            propietario.password,
            _configuration["Salt"]!);

        if (!isValid)
            return BadRequest(new { 
                Status = 400,
                Title = "Error de validación",
                Errors = new { currentPassword = new [] { "Contraseña actual incorrecta" } }
            });

        if (dto.NewPassword != dto.ConfirmPassword)
            return BadRequest(new { 
                Status = 400,
                Title = "Error de validación", 
                Errors = new { confirmPassword = new [] { "Las contraseñas no coinciden" } }
            });

        // Actualizar contraseña
        propietario.password = PasswordHasher.HashPassword(
            dto.NewPassword, 
            _configuration["Salt"]!);
            
        await _propietarioRepository.Update(propietario);
        return Ok(new { message = "Contraseña actualizada" });
    }

    private string GenerateJwtToken(Propietario propietario)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, propietario.id.ToString()),
            new Claim(ClaimTypes.Email, propietario.email),
            new Claim("FullName", $"{propietario.nombre} {propietario.apellido}")
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}