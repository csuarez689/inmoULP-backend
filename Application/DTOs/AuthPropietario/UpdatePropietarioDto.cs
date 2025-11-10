using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class UpdatePropietarioDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
    public string nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres")]
    public string apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(150, ErrorMessage = "El email no puede superar los 150 caracteres")]
    public string email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El teléfono debe tener entre 6 y 20 caracteres")]
    [RegularExpression(@"^[0-9+\-()\s]{6,20}$", ErrorMessage = "El teléfono solo puede contener números, espacios, +, - o paréntesis")]
    public string telefono { get; set; } = string.Empty;
}