#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IInmuebleRepository
{
    Task<Inmueble?> GetById(int id, bool? disponible = null);

    Task<List<Inmueble>> GetByPropietario(int propietarioId, bool? disponible = null);

    Task<int> Add(Inmueble inmueble);

    Task<int> Update(Inmueble inmueble);
}
