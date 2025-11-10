using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaAPI.Infrastructure.Repositories;

public class PagoRepository : IPagoRepository
{
    private readonly InmobiliariaDbContext _context;

    public PagoRepository(InmobiliariaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pago>> GetByContrato(int contratoId)
    {
        return await _context.Pagos
            .AsNoTracking()
            .Where(p => p.contrato_id == contratoId)
            .OrderByDescending(p => p.fechaPago)
            .ToListAsync();
    }
}
