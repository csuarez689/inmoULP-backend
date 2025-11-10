using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class PropietarioDto
{
    public string dni { get; set; }= string.Empty;
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string telefono { get; set; } = string.Empty;

    public static PropietarioDto FromEntity(Propietario propietario)
    {
        return new PropietarioDto
        {
            dni = propietario.dni,
            nombre = propietario.nombre,
            apellido = propietario.apellido,
            email = propietario.email,
            telefono = propietario.telefono
        };
    }
}