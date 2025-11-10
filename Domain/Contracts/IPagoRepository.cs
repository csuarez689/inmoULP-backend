using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IPagoRepository
{
    Task<List<Pago>> GetByContrato(int contratoId);
}
