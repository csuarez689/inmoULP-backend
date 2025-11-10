using System.ComponentModel.DataAnnotations;

namespace InmobiliariaAPI.Application.DTOs.Inmuebles;

public class UpdateInmuebleDto
{
    public string direccion { get; set; } = string.Empty;
    public int ambientes { get; set; }
    public int superficie { get; set; }
    public string latitud { get; set; } = string.Empty;
    public string longitud { get; set; } = string.Empty;
    public decimal precio { get; set; }
    public bool disponible { get; set; }
    public int tipo_id { get; set; }
    public int uso_id { get; set; }
}