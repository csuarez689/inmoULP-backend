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
    public string url { get; set; }

    [Column("id_inmueble")]
    public int id_inmueble { get; set; }

    [ForeignKey("id_inmueble")]
    public Inmueble Inmueble { get; set; }

}
