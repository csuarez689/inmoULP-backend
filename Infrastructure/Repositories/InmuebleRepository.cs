using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaAPI.Infrastructure.Repositories;

public class InmuebleRepository : IInmuebleRepository
{
    private readonly InmobiliariaDbContext _context;

    public InmuebleRepository(InmobiliariaDbContext context)
    {
        _context = context;
    }

    public async Task<Inmueble?> GetById(int id, bool? disponible = null)
    {
        var query = BaseQuery();

        if (disponible.HasValue)
            query = query.Where(i => i.disponible == disponible.Value);

        return await query.FirstOrDefaultAsync(i => i.id == id);
    }

    public async Task<List<Inmueble>> GetByPropietario(int propietarioId, bool? disponible = null)
    {
        var query = BaseQuery().Where(i => i.id_propietario == propietarioId);

        if (disponible.HasValue)
            query = query.Where(i => i.disponible == disponible.Value);

        return await query.ToListAsync();
    }

    public async Task<int> Add(Inmueble inmueble)
    {
        _context.Inmuebles.Add(inmueble);
        await _context.SaveChangesAsync();
        return inmueble.id;
    }

    public async Task<int> Update(Inmueble inmueble)
    {
        _context.Inmuebles.Update(inmueble);
        return await _context.SaveChangesAsync();
    }

    private IQueryable<Inmueble> BaseQuery()
    {
        return _context.Inmuebles
            .Include(i => i.Tipo)
            .Include(i => i.Uso)
            .AsQueryable();
    }
}
