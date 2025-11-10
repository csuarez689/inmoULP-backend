using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("imagenes_inmuebles")]
public class ImagenInmueble
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("url")]
    public string url { get; set; } = string.Empty;

    [Column("inmueble_id")]
    public int inmueble_id { get; set; }

    [ForeignKey("inmueble_id")]
    public Inmueble Inmueble { get; set; } = null!;

}
