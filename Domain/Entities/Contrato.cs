using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("contratos")]
public class Contrato
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("fecha_inicio")]
    public DateTime fechaInicio { get; set; }

    [Column("fecha_finalizacion")]
    public DateTime fechaFinalizacion { get; set; }

    [Column("monto_alquiler")]
    public double montoAlquiler { get; set; }

    [Column("inmueble_id")]
    public int inmueble_id { get; set; }

    [ForeignKey("inmueble_id")]
    public Inmueble Inmueble { get; set; }

    [Column("inquilino_id")]
    public int inquilino_id { get; set; }

    [ForeignKey("inquilino_id")]
    public Inquilino Inquilino { get; set; }

    [Column("estado")]
    public bool estado { get; set; }

    public ICollection<Pago> Pagos { get; set; }


}
