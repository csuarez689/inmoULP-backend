using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("usos_inmueble")]
public class UsoInmueble
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("nombre")]
    public string nombre { get; set; } = string.Empty;

    public ICollection<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();

}
