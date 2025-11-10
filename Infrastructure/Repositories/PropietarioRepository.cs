using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InmobiliariaAPI.Infrastructure.Repositories;

public class PropietarioRepository : IPropietarioRepository
{
    private readonly InmobiliariaDbContext _context;

    public PropietarioRepository(InmobiliariaDbContext context)
    {
        _context = context;
    }

    public async Task<Propietario?> GetById(int id, bool? activo = null)
    {
        var query = _context.Propietarios.AsQueryable();
        if (activo.HasValue)
        {
            query = query.Where(p => p.activo == activo.Value);
        }

        return await query.FirstOrDefaultAsync(p => p.id == id);
    }

    public async Task<Propietario?> GetByEmail(string email, bool? activo = null)
    {
        var query = _context.Propietarios.AsQueryable();
        if (activo.HasValue)
        {
            query = query.Where(p => p.activo == activo.Value);
        }

        return await query.FirstOrDefaultAsync(p => p.email == email);
    }

    public async Task<Propietario?> Update(Propietario propietario)
    {
        _context.Propietarios.Update(propietario);
        await _context.SaveChangesAsync();
        return propietario;
    }

    public async Task<bool> DniExists(string dni, int? excludeId = null)
    {
        var query = _context.Propietarios.Where(p => p.dni == dni);

        if (excludeId.HasValue)
            query = query.Where(p => p.id != excludeId.Value);

        return await query.AnyAsync();
    }

    public async Task<bool> EmailExists(string email, int? excludeId = null)
    {
        var query = _context.Propietarios.Where(p => p.email == email);

        if (excludeId.HasValue)
            query = query.Where(p => p.id != excludeId.Value);

        return await query.AnyAsync();
    }
}