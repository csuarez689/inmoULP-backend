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
        var existing = await _context.Propietarios.FindAsync(propietario.id);
        if (existing == null) return 0;

        // Actualización explícita de cada campo
        _context.Entry(existing).CurrentValues.SetValues(new {
            propietario.dni,
            propietario.nombre,
            propietario.apellido,
            propietario.email,
            propietario.telefono,
            propietario.password
        });

        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DniExists(string dni, int? excludeId = null)
    {
        var query = _context.Propietarios
            .Where(p => p.dni == dni && p.activo);
        
        if (excludeId.HasValue)
            query = query.Where(p => p.id != excludeId.Value);
        
        return await query.AnyAsync();
    }

    public async Task<bool> EmailExists(string email, int? excludeId = null)
    {
        var query = _context.Propietarios
            .Where(p => p.email == email && p.activo);
        
        if (excludeId.HasValue)
            query = query.Where(p => p.id != excludeId.Value);
        
        return await query.AnyAsync();
    }

}