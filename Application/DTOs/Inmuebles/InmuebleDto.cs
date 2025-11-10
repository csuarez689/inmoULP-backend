using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Application.DTOs.Inmuebles;

public class InmuebleDto
{
    public int id { get; set; }
    public string direccion { get; set; } = string.Empty;
    public int ambientes { get; set; }
    public int superficie { get; set; }
    public string latitud { get; set; } = string.Empty;
    public string longitud { get; set; } = string.Empty;
    public decimal precio { get; set; }
    public bool disponible { get; set; }
    
    // Datos relacionados
    public string tipo_inmueble { get; set; } = string.Empty;
    public string uso_inmueble { get; set; } = string.Empty;
    public string imagen_url { get; set; } = string.Empty;

    public static InmuebleDto FromEntity(Inmueble inmueble)
    {
        return new InmuebleDto
        {
            id = inmueble.id,
            direccion = inmueble.direccion,
            ambientes = inmueble.ambientes,
            superficie = inmueble.superficie,
            latitud = inmueble.latitud,
            longitud = inmueble.longitud,
            precio = inmueble.precio,
            disponible = inmueble.disponible,
            tipo_inmueble = inmueble.Tipo?.nombre ?? string.Empty,
            uso_inmueble = inmueble.Uso?.nombre ?? string.Empty,
            imagen_url = inmueble.Imagen?.url ?? string.Empty
        };
    }
}