#nullable enable

using System.Threading.Tasks;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Domain.Contracts;

public interface IPropietarioRepository
{
    Task<Propietario?> GetById(int id, bool? activo = null);

    Task<Propietario?> GetByEmail(string email, bool? activo = null);

    Task<Propietario?> Update(Propietario propietario);

    Task<bool> DniExists(string dni, int? excludeId = null);
    
    Task<bool> EmailExists(string email, int? excludeId = null);
}