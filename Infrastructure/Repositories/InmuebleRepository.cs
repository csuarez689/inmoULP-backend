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

    public async Task<Inmueble> Update(Inmueble inmueble)
    {
        _context.Inmuebles.Update(inmueble);
        await _context.SaveChangesAsync();
        return await GetById(inmueble.id);
    }



}
