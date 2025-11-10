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

    [HttpPost("resetpassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        var propietario = await _propietarioRepository.GetByEmail(request.Email, true);
        if (propietario != null)
        {
            const string newPassword = "123456";
            propietario.password = PasswordHasher.HashPassword(newPassword, _configuration["Salt"]!);
            await _propietarioRepository.Update(propietario);
        }
        //Simular envio de correo xD
        return Ok(new { message = "Se ha enviado un correo con instrucciones para reestablecer su contraseña" });
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

        if (!dto.dni.Equals(propietario.dni, StringComparison.Ordinal))
        {
            if (await _propietarioRepository.DniExists(dto.dni, propietario.id))
            {
                ModelState.AddModelError(nameof(dto.dni), "DNI ya registrado");
            }
            else
            {
                propietario.dni = dto.dni;
            }
        }

        if (!dto.email.Equals(propietario.email, StringComparison.OrdinalIgnoreCase))
        {
            if (await _propietarioRepository.EmailExists(dto.email, propietario.id))
            {
                ModelState.AddModelError(nameof(dto.email), "Email ya registrado");
            }
            else
            {
                propietario.email = dto.email;
            }
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        propietario.nombre = dto.nombre;
        propietario.apellido = dto.apellido;
        propietario.telefono = dto.telefono;

        var updated = await _propietarioRepository.Update(propietario);
        return updated != null
            ? Ok(PropietarioDto.FromEntity(updated))
            : StatusCode(500, new ProblemDetails { Title = "Error al actualizar" });
    }

    [HttpPut("me/changepassword")]
    [Authorize]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        
        // Validar contraseña actual
        if (!PasswordHasher.VerifyPassword(dto.CurrentPassword, propietario.password, _configuration["Salt"]!))
        {
            ModelState.AddModelError(nameof(dto.CurrentPassword), "Contraseña actual incorrecta");
        }

        if (dto.NewPassword != dto.ConfirmPassword)
        {
            ModelState.AddModelError(nameof(dto.ConfirmPassword), "Las contraseñas no coinciden");
        }

        if (dto.NewPassword == dto.CurrentPassword)
        {
            ModelState.AddModelError(nameof(dto.NewPassword), "La nueva contraseña debe ser diferente a la actual");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        // Actualizar contraseña
        propietario.password = PasswordHasher.HashPassword(dto.NewPassword, _configuration["Salt"]!);
        var result = await _propietarioRepository.Update(propietario);
        
        return result != null
            ? Ok(new { Message = "Contraseña actualizada" })
            : StatusCode(500, "Error al actualizar contraseña" );
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