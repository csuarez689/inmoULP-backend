using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaAPI.Domain.Entities;

[Table("pagos")]
public class Pago
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("detalle")]
    public string detalle { get; set; }

    [Column("fecha_pago")]
    public DateTime fechaPago { get; set; }

    [Column("monto")]
    public double monto { get; set; }

    [Column("estado")]
    public bool estado { get; set; }

    [Column("contrato_id")]
    public int contrato_id { get; set; }

    [ForeignKey("contrato_id")]
    public Contrato Contrato { get; set; }
}
