using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("tipos_inmueble")]
public class TipoInmueble
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("nombre")]
    public string nombre { get; set; }

    public ICollection<Inmueble> Inmuebles { get; set; }
}
