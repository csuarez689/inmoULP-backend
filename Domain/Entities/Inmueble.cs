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
    public string latitud { get; set; }

    [Column("longitud")]
    public string longitud { get; set; }

    [Column("tipo_id")]
    public int tipo_id { get; set; }

    [ForeignKey("tipo_id")]
    public TipoInmueble Tipo { get; set; }

    [Column("uso_id")]
    public int uso_id { get; set; }

    [ForeignKey("uso_id")]
    public UsoInmueble Uso { get; set; }

    [Column("propietario_id")]
    public int propietario_id { get; set; }

    [ForeignKey("propietario_id")]
    public Propietario Propietario { get; set; }

    [Column("disponible")]
    public bool disponible { get; set; } = true;

    [Column("precio")]
    public decimal precio { get; set; }


    public ICollection<Contrato> Contratos { get; set; }
    
    public virtual ImagenInmueble? Imagen { get; set; } 

}
