using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IContratoRepository
{
    Task<List<Contrato>> GetByInmueble(int inmuebleId);
    Task<Contrato?> GetById(int contratoId);
}