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

    Task<Inmueble> Create(Inmueble inmueble);

    Task<Inmueble?> SetImagenUrl(int inmuebleId, string url);

    Task<bool> TipoExists(int tipoId);

    Task<bool> UsoExists(int usoId);

    Task<List<TipoInmueble>> GetTipos();

    Task<List<UsoInmueble>> GetUsos();

    Task<List<Inmueble>> GetConContratosVigentes(int propietarioId);
}
