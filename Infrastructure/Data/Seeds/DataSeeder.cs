using System.Collections.Generic;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Domain.Enums;
using InmobiliariaAPI.Application.Utils;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaAPI.Infrastructure.Data.Seeds;

public static class DataSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        // Hashear password con el mismo salt que está en appsettings.json
        const string salt = "h7H9gK2pQz8LwS6rXjD4fN1tVbY0eU";
        var hash = PasswordHasher.HashPassword("admin123", salt);
        
        modelBuilder.Entity<Usuario>().HasData(new Usuario
        {
            id = 1,
            nombre = "Admin",
            apellido = "Sistema",
            email = "admin@inmobiliaria.com",
            password = hash,
            rol = RolUsuario.Administrador
        });

        var propietarios = new List<Propietario>
        {
            new()
            {
                id = 1,
                dni = 20123456,
                nombre = "Carlos",
                apellido = "Gómez",
                email = "carlos.gomez@example.com",
                telefono = "2664000001",
                password = hash,
                activo = true
            },
            new()
            {
                id = 2,
                dni = 20987654,
                nombre = "María",
                apellido = "Fernández",
                email = "maria.fernandez@example.com",
                telefono = "2664000002",
                password = hash,
                activo = true
            },
            new()
            {
                id = 3,
                dni = 19543210,
                nombre = "Lucía",
                apellido = "Pérez",
                email = "lucia.perez@example.com",
                telefono = "2664000003",
                password = hash,
                activo = true
            },
            new()
            {
                id = 4,
                dni = 21456789,
                nombre = "Javier",
                apellido = "Rodríguez",
                email = "javier.rodriguez@example.com",
                telefono = "2664000004",
                password = hash,
                activo = true
            },
            new()
            {
                id = 5,
                dni = 22345678,
                nombre = "Ana",
                apellido = "Lopez",
                email = "ana.lopez@example.com",
                telefono = "2664000005",
                password = hash,
                activo = true
            }
        };

        modelBuilder.Entity<Propietario>().HasData(propietarios);
    }
}
