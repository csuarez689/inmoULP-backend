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

    [Column("id_inmueble")]
    public int id_inmueble { get; set; }

    [ForeignKey("id_inmueble")]
    public Inmueble Inmueble { get; set; }

    [Column("id_inquilino")]
    public int id_inquilino { get; set; }

    [ForeignKey("id_inquilino")]
    public Inquilino Inquilino { get; set; }

    [Column("estado")]
    public bool estado { get; set; }

    public ICollection<Pago> Pagos { get; set; }


}
