#nullable enable

using System.Threading.Tasks;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IPropietarioRepository
{
    Task<Propietario?> GetById(int id, bool? activo = null);

    Task<Propietario?> GetByEmail(string email, bool? activo = null);

    Task<int> Update(Propietario propietario);
}