using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class LoginRequestDto
{
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "El email debe tener entre 6 y 100 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 50 caracteres")]
    public string Password { get; set; } = string.Empty;
}