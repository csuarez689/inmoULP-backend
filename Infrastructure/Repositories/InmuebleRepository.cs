using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InmobiliariaAPI.Infrastructure.Repositories;

public class InmuebleRepository : IInmuebleRepository
{
    private readonly InmobiliariaDbContext _context;

    public InmuebleRepository(InmobiliariaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inmueble>> GetByPropietario(int propietarioId)
    {
        return await _context.Inmuebles
            .Include(i => i.Tipo)
            .Include(i => i.Uso)
            .Include(i => i.Imagen)
            .Where(i => i.propietario_id == propietarioId)
            .OrderByDescending(i => i.id)
            .ToListAsync();
    }

    public async Task<Inmueble?> GetById(int id)
    {
        return await _context.Inmuebles
            .Include(i => i.Tipo)
            .Include(i => i.Uso)
            .Include(i => i.Imagen)
            .FirstOrDefaultAsync(i => i.id == id);
    }

    public async Task<Inmueble?> Update(Inmueble inmueble)
    {
        _context.Inmuebles.Update(inmueble);
        await _context.SaveChangesAsync();
        return await GetById(inmueble.id);
    }

    public async Task<Inmueble> Create(Inmueble inmueble)
    {
        _context.Inmuebles.Add(inmueble);
        await _context.SaveChangesAsync();
        return await GetById(inmueble.id) ?? inmueble;
    }

    public async Task<Inmueble?> SetImagenUrl(int inmuebleId, string url)
    {
        var inmueble = await _context.Inmuebles.FindAsync(inmuebleId);
        if (inmueble == null)
            return null;

        var imagen = await _context.ImagenesInmuebles
            .FirstOrDefaultAsync(i => i.inmueble_id == inmuebleId);

        if (imagen == null)
        {
            imagen = new ImagenInmueble
            {
                inmueble_id = inmuebleId,
                url = url
            };
            _context.ImagenesInmuebles.Add(imagen);
        }
        else
        {
            imagen.url = url;
            _context.ImagenesInmuebles.Update(imagen);
        }

        await _context.SaveChangesAsync();
        return await GetById(inmuebleId);
    }

    public async Task<bool> TipoExists(int tipoId)
    {
        return await _context.TiposInmueble.AnyAsync(t => t.id == tipoId);
    }

    public async Task<bool> UsoExists(int usoId)
    {
        return await _context.UsosInmueble.AnyAsync(u => u.id == usoId);
    }

    public async Task<List<TipoInmueble>> GetTipos()
    {
        return await _context.TiposInmueble
            .OrderBy(t => t.nombre)
            .ToListAsync();
    }

    public async Task<List<UsoInmueble>> GetUsos()
    {
        return await _context.UsosInmueble
            .OrderBy(u => u.nombre)
            .ToListAsync();
    }

    public async Task<List<Inmueble>> GetConContratosVigentes(int propietarioId)
    {
        var hoy = DateTime.UtcNow.Date;

        return await _context.Inmuebles
            .Include(i => i.Tipo)
            .Include(i => i.Uso)
            .Include(i => i.Imagen)
            .Where(i => i.propietario_id == propietarioId)
            .Where(i => i.Contratos.Any(c => c.estado && c.fechaInicio <= hoy && c.fechaFinalizacion >= hoy))
            .ToListAsync();
    }
}
