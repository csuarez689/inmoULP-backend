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

    public async Task<int> Update(Propietario propietario)
    {
        _context.Propietarios.Update(propietario);
        return await _context.SaveChangesAsync();
    }

}