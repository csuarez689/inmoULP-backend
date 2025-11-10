using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class ResetPasswordDto
{
    private string _email = string.Empty;

    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "El email debe tener entre 6 y 100 caracteres")]
    public string Email
    {
        get => _email;
        set => _email = (value ?? string.Empty).Trim();
    }
}
