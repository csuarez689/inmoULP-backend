#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IPropietarioRepository
{
    Task<List<Propietario>> GetAll(bool? activo = null);
    Task<Propietario?> GetById(int id, bool? activo = null);
    Task<Propietario?> GetByEmail(string email, bool? activo = null);
    Task<int> Add(Propietario propietario);
    Task<int> Update(Propietario propietario);
    Task<int> Delete(int id);
}