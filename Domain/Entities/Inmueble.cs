using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("inmuebles")]
public class Inmueble
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("direccion")]
    public string direccion { get; set; }

    [Column("ambientes")]
    public int ambientes { get; set; }

    [Column("superficie")]
    public int superficie { get; set; }

    [Column("latitud")]
    public decimal latitud { get; set; }

    [Column("longitud")]
    public decimal longitud { get; set; }

    [Column("id_tipo")]
    public int id_tipo { get; set; }

    [ForeignKey("id_tipo")]
    public TipoInmueble Tipo { get; set; }

    [Column("id_uso")]
    public int id_uso { get; set; }

    [ForeignKey("id_uso")]
    public UsoInmueble Uso { get; set; }

    [Column("disponible")]
    public bool disponible { get; set; } = true;

    [Column("precio")]
    public decimal precio { get; set; }

    [Column("id_propietario")]
    public int id_propietario { get; set; }

    [ForeignKey("id_propietario")]
    public Propietario Propietario { get; set; }

    public ICollection<Contrato> Contratos { get; set; }
    
    public ICollection<ImagenInmueble> Imagenes { get; set; }

}
