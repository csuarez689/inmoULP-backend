using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("propietarios")]
public class Propietario
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("dni")]
    public string dni { get; set; } = string.Empty;

    [Column("nombre")]
    public string nombre { get; set; } = string.Empty;

    [Column("apellido")]
    public string apellido { get; set; } = string.Empty;

    [Column("email")]
    public string email { get; set; } = string.Empty;

    [Column("telefono")]
    public string telefono { get; set; } = string.Empty;

    [Column("password")]
    public string password { get; set; } = string.Empty;

    [Column("activo")]
    public bool activo { get; set; } = true;

    public ICollection<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();

}
