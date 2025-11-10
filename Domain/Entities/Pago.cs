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

    [Column("id_contrato")]
    public int id_contrato { get; set; }

    [ForeignKey("id_contrato")]
    public Contrato Contrato { get; set; }
}
