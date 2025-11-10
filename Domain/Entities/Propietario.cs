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
    public string dni { get; set; }

    [Column("nombre")]
    public string nombre { get; set; }

    [Column("apellido")]
    public string apellido { get; set; }

    [Column("email")]
    public string email { get; set; }

    [Column("telefono")]
    public string telefono { get; set; }

    [Column("password")]
    public string password { get; set; }

    [Column("activo")]
    public bool activo { get; set; } = true;

    public ICollection<Inmueble> Inmuebles { get; set; }

}
