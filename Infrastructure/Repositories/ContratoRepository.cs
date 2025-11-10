using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaAPI.Infrastructure.Repositories;

public class ContratoRepository : IContratoRepository
{
    private readonly InmobiliariaDbContext _context;

    public ContratoRepository(InmobiliariaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contrato>> GetByInmueble(int inmuebleId)
    {
        return await _context.Contratos
            .AsNoTracking()
            .Include(c => c.Inquilino)
            .Include(c => c.Inmueble)
                .ThenInclude(i => i.Tipo)
            .Include(c => c.Inmueble)
                .ThenInclude(i => i.Uso)
            .Include(c => c.Inmueble)
                .ThenInclude(i => i.Imagen)
            .Where(c => c.inmueble_id == inmuebleId)
            .OrderByDescending(c => c.fechaInicio)
            .ToListAsync();
    }

    public async Task<Contrato?> GetById(int contratoId)
    {
        return await _context.Contratos
            .AsNoTracking()
            .Include(c => c.Inmueble)
                .ThenInclude(i => i.Propietario)
            .Include(c => c.Inquilino)
            .FirstOrDefaultAsync(c => c.id == contratoId);
    }
}
