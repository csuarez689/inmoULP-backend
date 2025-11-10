using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.Inmuebles;

public class CreateInmuebleDto
{
    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
    public string direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los ambientes son obligatorios")]
    [Range(1, 50, ErrorMessage = "Los ambientes deben estar entre 1 y 50")]
    public int ambientes { get; set; }

    [Required(ErrorMessage = "La superficie es obligatoria")]
    [Range(1, 10000, ErrorMessage = "La superficie debe estar entre 1 y 10000 m²")]
    public int superficie { get; set; }

    [Required(ErrorMessage = "La latitud es obligatoria")]
    public string latitud { get; set; } = string.Empty;

    [Required(ErrorMessage = "La longitud es obligatoria")]
    public string longitud { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal precio { get; set; }

    [Required(ErrorMessage = "El tipo de inmueble es obligatorio")]
    public int tipo_id { get; set; }

    [Required(ErrorMessage = "El uso del inmueble es obligatorio")]
    public int uso_id { get; set; }

    public IFormFile? imagen { get; set; }
}