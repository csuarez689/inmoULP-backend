using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class UpdatePropietarioDto
{
    private string _dni = string.Empty;
    private string _nombre = string.Empty;
    private string _apellido = string.Empty;
    private string _email = string.Empty;
    private string _telefono = string.Empty;

    [Required(ErrorMessage = "El DNI es requerido")]
    [StringLength(9, MinimumLength = 7, ErrorMessage = "El DNI debe tener entre 7 y 9 caracteres")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "El DNI solo debe contener números")]
    public string dni
    {
        get => _dni;
        set => _dni = (value ?? string.Empty).Trim();
    }
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string nombre
    {
        get => _nombre;
        set => _nombre = (value ?? string.Empty).Trim();
    }
    
    [Required(ErrorMessage = "El apellido es requerido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
    public string apellido
    {
        get => _apellido;
        set => _apellido = (value ?? string.Empty).Trim();
    }
    
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "El email debe tener entre 10 y 100 caracteres")]
    public string email
    {
        get => _email;
        set => _email = (value ?? string.Empty).Trim();
    }
    
    [StringLength(20, MinimumLength = 10, ErrorMessage = "El teléfono debe tener entre 10 y 20 caracteres")]
    public string telefono
    {
        get => _telefono;
        set => _telefono = (value ?? string.Empty).Trim();
    }
}