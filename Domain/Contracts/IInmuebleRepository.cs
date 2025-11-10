#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IInmuebleRepository
{
    Task<List<Inmueble>> GetByPropietario(int propietarioId);

    Task<Inmueble?> GetById(int id);

    Task<Inmueble?> Update(Inmueble inmueble);
    
}
