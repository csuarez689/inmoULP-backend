using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class LoginRequestDto
{
    private string _email = string.Empty;
    private string _password = string.Empty;

    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "El email debe tener entre 6 y 100 caracteres")]
    public string Email
    {
        get => _email;
        set => _email = (value ?? string.Empty).Trim();
    }

    [Required(ErrorMessage = "La contraseña es requerida")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 50 caracteres")]
    public string Password
    {
        get => _password;
        set => _password = (value ?? string.Empty).Trim();
    }
}