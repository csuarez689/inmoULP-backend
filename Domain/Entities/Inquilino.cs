using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("inquilinos")]
public class Inquilino
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("dni", TypeName = "varchar(15)")] 
    public string dni { get; set; } = string.Empty;

    [Column("nombre")]
    public string nombre { get; set; } = string.Empty;

    [Column("apellido")]
    public string apellido { get; set; } = string.Empty;

    [Column("email")]
    public string email { get; set; } = string.Empty;

    [Column("telefono")]
    public string telefono { get; set; } = string.Empty;

    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
