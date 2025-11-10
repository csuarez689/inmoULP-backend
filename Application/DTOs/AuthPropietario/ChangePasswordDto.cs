using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "La contraseña actual es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña actual debe tener entre 6 y 50 caracteres")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La nueva contraseña debe tener entre 6 y 50 caracteres")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "La confirmación de la nueva contraseña es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La confirmación de la nueva contraseña debe tener entre 6 y 50 caracteres")]
    [Compare(nameof(NewPassword), ErrorMessage = "La confirmación no coincide con la nueva contraseña")]
    public string ConfirmPassword { get; set; } = string.Empty;
}