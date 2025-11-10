namespace InmobiliariaAPI.Application.DTOs.AuthPropietario;

public class PropietarioDto
{
    public int dni { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string telefono { get; set; } = string.Empty;
}