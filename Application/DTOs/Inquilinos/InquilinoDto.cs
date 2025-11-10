using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Application.DTOs.Inquilinos;

public class InquilinoDto
{
    public int id { get; set; }
    public string dni { get; set; } = string.Empty;
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string telefono { get; set; } = string.Empty;

    public static InquilinoDto FromEntity(Inquilino inquilino)
    {
        return new InquilinoDto
        {
            id = inquilino.id,
            dni = inquilino.dni,
            nombre = inquilino.nombre,
            apellido = inquilino.apellido,
            email = inquilino.email,
            telefono = inquilino.telefono
        };
    }
}
