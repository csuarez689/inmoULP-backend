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

    public async Task<List<Propietario>> GetAll(bool? activo = null)
    {
        var query = _context.Propietarios.AsQueryable();
        if (activo.HasValue)
        {
            query = query.Where(p => p.activo == activo.Value);
        }

        return await query.ToListAsync();
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

    public async Task<int> Add(Propietario propietario)
    {
        _context.Propietarios.Add(propietario);
        await _context.SaveChangesAsync();
        return propietario.id;
    }

    public async Task<int> Update(Propietario propietario)
    {
        _context.Propietarios.Update(propietario);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        var propietario = await GetById(id);

        if (propietario != null)
        {
            _context.Propietarios.Remove(propietario);
            return await _context.SaveChangesAsync();
        }
        return 0;
    }
}