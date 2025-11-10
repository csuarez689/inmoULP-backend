using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("inquilinos")]
public class Inquilino
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("dni")]
    public int dni { get; set; }

    [Column("nombre")]
    public string nombre { get; set; }

    [Column("apellido")]
    public string apellido { get; set; }

    [Column("email")]
    public string email { get; set; }

    [Column("telefono")]
    public string telefono { get; set; }

    public ICollection<Contrato> Contratos { get; set; }
}
