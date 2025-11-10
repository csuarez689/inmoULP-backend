using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class ChangePasswordDto
{
    private string _currentPassword = string.Empty;
    private string _newPassword = string.Empty;
    private string _confirmPassword = string.Empty;

    [Required(ErrorMessage = "La contraseña actual es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña actual debe tener entre 6 y 50 caracteres")]
    public string CurrentPassword
    {
        get => _currentPassword;
        set => _currentPassword = (value ?? string.Empty).Trim();
    }

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La nueva contraseña debe tener entre 6 y 50 caracteres")]
    public string NewPassword
    {
        get => _newPassword;
        set => _newPassword = (value ?? string.Empty).Trim();
    }

    [Required(ErrorMessage = "La confirmación de la nueva contraseña es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La confirmación de la nueva contraseña debe tener entre 6 y 50 caracteres")]
    [Compare(nameof(NewPassword), ErrorMessage = "La confirmación no coincide con la nueva contraseña")]
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => _confirmPassword = (value ?? string.Empty).Trim();
    }
}