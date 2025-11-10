using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class UpdatePropietarioDto
{
    [Required(ErrorMessage = "El DNI es requerido")]
    [StringLength(9, MinimumLength = 7, ErrorMessage = "El DNI debe tener entre 7 y 9 caracteres")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "El DNI solo debe contener números")]
    public string dni { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100,MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string nombre { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El apellido es requerido")] 
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
    public string apellido { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "El email debe tener entre 10 y 100 caracteres")]
    public string email { get; set; } = string.Empty;
    
    [StringLength(20, MinimumLength = 10, ErrorMessage = "El teléfono debe tener entre 6 y 20 caracteres")]
    public string telefono { get; set; } = string.Empty;
}