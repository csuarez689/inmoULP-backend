using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Application.DTOs.Pagos;

public class PagoDto
{
    public int Id { get; init; }
    public string Detalle { get; init; } = string.Empty;
    public DateTime FechaPago { get; init; }
    public double Monto { get; init; }
    public bool Estado { get; init; }

    public static PagoDto FromEntity(Pago pago)
    {
        return new PagoDto
        {
            Id = pago.id,
            Detalle = pago.detalle,
            FechaPago = pago.fechaPago,
            Monto = pago.monto,
            Estado = pago.estado
        };
    }
}
