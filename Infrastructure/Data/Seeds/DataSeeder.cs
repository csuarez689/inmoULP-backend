using System.Collections.Generic;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Application.Utils;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaAPI.Infrastructure.Data.Seeds;

public static class DataSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        SeedPropietarios(modelBuilder);
        SeedTiposInmueble(modelBuilder);
        SeedUsosInmueble(modelBuilder);
        SeedInmuebles(modelBuilder);
        SeedImagenes(modelBuilder);
        SeedInquilinos(modelBuilder);
        SeedContratos(modelBuilder);
        SeedPagos(modelBuilder);
    }

    private static void SeedPropietarios(ModelBuilder modelBuilder)
    {
        // hashear password con el mismo salt que está en appsettings.json
        const string salt = "h7H9gK2pQz8LwS6rXjD4fN1tVbY0eU";
        var hash = PasswordHasher.HashPassword("admin123", salt);

        var propietarios = new List<Propietario>
        {
            new()
            {
                id = 1,
                dni = "20123456",
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
                dni = "20987654",
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
                dni = "19543210",
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
                dni = "21456789",
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
                dni = "22345678",
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

    private static void SeedTiposInmueble(ModelBuilder modelBuilder)
    {
        var tipos = new List<TipoInmueble>
        {
            new() { id = 1, nombre = "Casa" },
            new() { id = 2, nombre = "Departamento" },
            new() { id = 3, nombre = "Local Comercial" },
            new() { id = 4, nombre = "Oficina" }
        };

        modelBuilder.Entity<TipoInmueble>().HasData(tipos);
    }

    private static void SeedUsosInmueble(ModelBuilder modelBuilder)
    {
        var usos = new List<UsoInmueble>
        {
            new() { id = 1, nombre = "Residencial" },
            new() { id = 2, nombre = "Comercial" }
        };

        modelBuilder.Entity<UsoInmueble>().HasData(usos);
    }

    private static void SeedInmuebles(ModelBuilder modelBuilder)
    {
        var inmuebles = new List<Inmueble>
        {
            // Propietario 1
            new() { id = 1, direccion = "Av. España 102", ambientes = 4, superficie = 140, latitud = "-33.296512", longitud = "-66.335421", tipo_id = 1, uso_id = 1, disponible = false, precio = 120000, propietario_id = 1 },
            new() { id = 2, direccion = "Belgrano 450", ambientes = 3, superficie = 95, latitud = "-33.297845", longitud = "-66.333210", tipo_id = 2, uso_id = 1, disponible = true, precio = 98000, propietario_id = 1 },
            new() { id = 3, direccion = "Junín 780", ambientes = 2, superficie = 70, latitud = "-33.298762", longitud = "-66.331567", tipo_id = 2, uso_id = 1, disponible = false, precio = 75000, propietario_id = 1 },
            new() { id = 4, direccion = "Mitre 1200", ambientes = 5, superficie = 180, latitud = "-33.299843", longitud = "-66.336789", tipo_id = 1, uso_id = 1, disponible = false, precio = 145000, propietario_id = 1 },
            new() { id = 5, direccion = "Colón 350", ambientes = 3, superficie = 110, latitud = "-33.301256", longitud = "-66.334123", tipo_id = 3, uso_id = 2, disponible = false, precio = 210000, propietario_id = 1 },

            // Propietario 2
            new() { id = 6, direccion = "Buenos Aires 89", ambientes = 4, superficie = 160, latitud = "-33.305612", longitud = "-66.337456", tipo_id = 1, uso_id = 1, disponible = true, precio = 152000, propietario_id = 2 },
            new() { id = 7, direccion = "Chacabuco 560", ambientes = 3, superficie = 100, latitud = "-33.304123", longitud = "-66.332345", tipo_id = 2, uso_id = 1, disponible = true, precio = 99000, propietario_id = 2 },
            new() { id = 8, direccion = "San Martín 910", ambientes = 2, superficie = 75, latitud = "-33.302981", longitud = "-66.329876", tipo_id = 2, uso_id = 1, disponible = false, precio = 82000, propietario_id = 2 },
            new() { id = 9, direccion = "Rivadavia 2100", ambientes = 5, superficie = 200, latitud = "-33.306745", longitud = "-66.340012", tipo_id = 1, uso_id = 1, disponible = false, precio = 165000, propietario_id = 2 },
            new() { id = 10, direccion = "Pringles 45", ambientes = 4, superficie = 150, latitud = "-33.303567", longitud = "-66.327543", tipo_id = 3, uso_id = 2, disponible = true, precio = 230000, propietario_id = 2 },

            // Propietario 3
            new() { id = 11, direccion = "Pueyrredón 320", ambientes = 3, superficie = 120, latitud = "-33.312345", longitud = "-66.341234", tipo_id = 1, uso_id = 1, disponible = true, precio = 138000, propietario_id = 3 },
            new() { id = 12, direccion = "Ituzaingó 654", ambientes = 2, superficie = 68, latitud = "-33.310234", longitud = "-66.338765", tipo_id = 2, uso_id = 1, disponible = true, precio = 87000, propietario_id = 3 },
            new() { id = 13, direccion = "Catamarca 870", ambientes = 4, superficie = 140, latitud = "-33.311789", longitud = "-66.336543", tipo_id = 1, uso_id = 1, disponible = false, precio = 149000, propietario_id = 3 },
            new() { id = 14, direccion = "Lafinur 120", ambientes = 1, superficie = 55, latitud = "-33.309432", longitud = "-66.333210", tipo_id = 4, uso_id = 2, disponible = true, precio = 110000, propietario_id = 3 },
            new() { id = 15, direccion = "Ayacucho 410", ambientes = 3, superficie = 105, latitud = "-33.308765", longitud = "-66.331098", tipo_id = 2, uso_id = 1, disponible = false, precio = 96000, propietario_id = 3 },

            // Propietario 4
            new() { id = 16, direccion = "Independencia 1500", ambientes = 4, superficie = 150, latitud = "-33.318765", longitud = "-66.346543", tipo_id = 1, uso_id = 1, disponible = true, precio = 155000, propietario_id = 4 },
            new() { id = 17, direccion = "Balcarce 620", ambientes = 3, superficie = 98, latitud = "-33.316234", longitud = "-66.342101", tipo_id = 2, uso_id = 1, disponible = true, precio = 93000, propietario_id = 4 },
            new() { id = 18, direccion = "Los Puquios 45", ambientes = 2, superficie = 65, latitud = "-33.320987", longitud = "-66.347890", tipo_id = 2, uso_id = 1, disponible = false, precio = 78000, propietario_id = 4 },
            new() { id = 19, direccion = "Illia 3450", ambientes = 5, superficie = 210, latitud = "-33.317654", longitud = "-66.349321", tipo_id = 1, uso_id = 1, disponible = false, precio = 172000, propietario_id = 4 },
            new() { id = 20, direccion = "Ruta 3 Km 5", ambientes = 4, superficie = 190, latitud = "-33.322345", longitud = "-66.351234", tipo_id = 3, uso_id = 2, disponible = true, precio = 240000, propietario_id = 4 },

            // Propietario 5
            new() { id = 21, direccion = "Esteban Adaro 890", ambientes = 3, superficie = 115, latitud = "-33.325678", longitud = "-66.353456", tipo_id = 1, uso_id = 1, disponible = true, precio = 142000, propietario_id = 5 },
            new() { id = 22, direccion = "Concarán 120", ambientes = 2, superficie = 82, latitud = "-33.324321", longitud = "-66.356789", tipo_id = 2, uso_id = 1, disponible = true, precio = 88000, propietario_id = 5 },
            new() { id = 23, direccion = "Fraga 630", ambientes = 1, superficie = 50, latitud = "-33.326543", longitud = "-66.358901", tipo_id = 4, uso_id = 2, disponible = false, precio = 105000, propietario_id = 5 },
            new() { id = 24, direccion = "El Trapiche 270", ambientes = 4, superficie = 160, latitud = "-33.327890", longitud = "-66.360123", tipo_id = 1, uso_id = 1, disponible = false, precio = 158000, propietario_id = 5 },
            new() { id = 25, direccion = "Potrero de los Funes 410", ambientes = 3, superficie = 130, latitud = "-33.329012", longitud = "-66.362345", tipo_id = 3, uso_id = 2, disponible = false, precio = 215000, propietario_id = 5 },

            // Propietario 1 - adicionales
            new() { id = 26, direccion = "Riobamba 640", ambientes = 3, superficie = 118, latitud = "-33.300432", longitud = "-66.332654", tipo_id = 2, uso_id = 1, disponible = false, precio = 99000, propietario_id = 1 },
            new() { id = 27, direccion = "España 520", ambientes = 5, superficie = 175, latitud = "-33.302145", longitud = "-66.334789", tipo_id = 1, uso_id = 1, disponible = false, precio = 168000, propietario_id = 1 }
        };

        modelBuilder.Entity<Inmueble>().HasData(inmuebles);
    }

    private static void SeedImagenes(ModelBuilder modelBuilder)
    {
        var imagenes = new List<ImagenInmueble>
        {
            new() { id = 1, inmueble_id = 1, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 2, inmueble_id = 2, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 3, inmueble_id = 3, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 4, inmueble_id = 4, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 5, inmueble_id = 5, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 6, inmueble_id = 6, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 7, inmueble_id = 7, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 8, inmueble_id = 8, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 9, inmueble_id = 9, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 10, inmueble_id = 10, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 11, inmueble_id = 11, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 12, inmueble_id = 12, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 13, inmueble_id = 13, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 14, inmueble_id = 14, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 15, inmueble_id = 15, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 16, inmueble_id = 16, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 17, inmueble_id = 17, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 18, inmueble_id = 18, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 19, inmueble_id = 19, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 20, inmueble_id = 20, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 21, inmueble_id = 21, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 22, inmueble_id = 22, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 23, inmueble_id = 23, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 24, inmueble_id = 24, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 25, inmueble_id = 25, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 26, inmueble_id = 26, url = "/uploads/inmuebles/test_casa.jpg" },
            new() { id = 27, inmueble_id = 27, url = "/uploads/inmuebles/test_casa.jpg" }
        };

        modelBuilder.Entity<ImagenInmueble>().HasData(imagenes);
    }


    private static void SeedInquilinos(ModelBuilder modelBuilder)
    {
        var inquilinos = new List<Inquilino>
        {
            new() { id = 1, dni = "30111222", nombre = "Juan", apellido = "Pérez", email = "juan.perez@example.com", telefono = "2664100001" },
            new() { id = 2, dni = "30999888", nombre = "Laura", apellido = "Giménez", email = "laura.gimenez@example.com", telefono = "2664100002" },
            new() { id = 3, dni = "32123456", nombre = "Martín", apellido = "Sosa", email = "martin.sosa@example.com", telefono = "2664100003" },
            new() { id = 4, dni = "33987654", nombre = "Carolina", apellido = "Molina", email = "carolina.molina@example.com", telefono = "2664100004" },
            new() { id = 5, dni = "34111222", nombre = "Pablo", apellido = "Ríos", email = "pablo.rios@example.com", telefono = "2664100005" },
            new() { id = 6, dni = "35222333", nombre = "Jimena", apellido = "Silva", email = "jimena.silva@example.com", telefono = "2664100006" },
            new() { id = 7, dni = "36888999", nombre = "Ricardo", apellido = "López", email = "ricardo.lopez@example.com", telefono = "2664100007" },
            new() { id = 8, dni = "37876543", nombre = "Sofía", apellido = "Herrera", email = "sofia.herrera@example.com", telefono = "2664100008" },
            new() { id = 9, dni = "38901234", nombre = "Valeria", apellido = "Torres", email = "valeria.torres@example.com", telefono = "2664100009" },
            new() { id = 10, dni = "39990123", nombre = "Diego", apellido = "Navarro", email = "diego.navarro@example.com", telefono = "2664100010" },
            new() { id = 11, dni = "40987654", nombre = "Marina", apellido = "Ponce", email = "marina.ponce@example.com", telefono = "2664100011" },
            new() { id = 12, dni = "41223344", nombre = "Santiago", apellido = "Giuliani", email = "santiago.giuliani@example.com", telefono = "2664100012" },
            new() { id = 13, dni = "42556677", nombre = "Celeste", apellido = "Moreno", email = "celeste.moreno@example.com", telefono = "2664100013" },
            new() { id = 14, dni = "43667788", nombre = "Federico", apellido = "Arias", email = "federico.arias@example.com", telefono = "2664100014" },
            new() { id = 15, dni = "44778899", nombre = "Luciana", apellido = "Correa", email = "luciana.correa@example.com", telefono = "2664100015" }
        };

        modelBuilder.Entity<Inquilino>().HasData(inquilinos);
    }

    private static void SeedContratos(ModelBuilder modelBuilder)
    {
        var contratos = new List<Contrato>
        {
            // Vigentes (estado true)
            new() { id = 1, inmueble_id = 1, inquilino_id = 1, fechaInicio = new DateTime(2024, 1, 1), fechaFinalizacion = new DateTime(2026, 12, 31), montoAlquiler = 120000, estado = true },
            new() { id = 2, inmueble_id = 3, inquilino_id = 2, fechaInicio = new DateTime(2023, 5, 1), fechaFinalizacion = new DateTime(2025, 4, 30), montoAlquiler = 95000, estado = true },
            new() { id = 3, inmueble_id = 4, inquilino_id = 11, fechaInicio = new DateTime(2024, 7, 1), fechaFinalizacion = new DateTime(2026, 6, 30), montoAlquiler = 140000, estado = true },
            new() { id = 4, inmueble_id = 26, inquilino_id = 12, fechaInicio = new DateTime(2023, 11, 1), fechaFinalizacion = new DateTime(2025, 10, 31), montoAlquiler = 99000, estado = true },
            new() { id = 5, inmueble_id = 27, inquilino_id = 13, fechaInicio = new DateTime(2024, 2, 1), fechaFinalizacion = new DateTime(2026, 1, 31), montoAlquiler = 155000, estado = true },
            new() { id = 6, inmueble_id = 8, inquilino_id = 14, fechaInicio = new DateTime(2024, 3, 1), fechaFinalizacion = new DateTime(2026, 2, 28), montoAlquiler = 78000, estado = true },
            new() { id = 7, inmueble_id = 19, inquilino_id = 15, fechaInicio = new DateTime(2024, 5, 1), fechaFinalizacion = new DateTime(2026, 4, 30), montoAlquiler = 132000, estado = true },

            // Vencidos (estado false)
            new() { id = 8, inmueble_id = 2, inquilino_id = 3, fechaInicio = new DateTime(2020, 1, 1), fechaFinalizacion = new DateTime(2022, 12, 31), montoAlquiler = 55000, estado = false },
            new() { id = 9, inmueble_id = 5, inquilino_id = 4, fechaInicio = new DateTime(2019, 6, 1), fechaFinalizacion = new DateTime(2021, 5, 31), montoAlquiler = 68000, estado = false },
            new() { id = 10, inmueble_id = 7, inquilino_id = 5, fechaInicio = new DateTime(2018, 3, 1), fechaFinalizacion = new DateTime(2020, 2, 29), montoAlquiler = 63000, estado = false },
            new() { id = 11, inmueble_id = 10, inquilino_id = 6, fechaInicio = new DateTime(2019, 9, 1), fechaFinalizacion = new DateTime(2021, 8, 31), montoAlquiler = 80000, estado = false },
            new() { id = 12, inmueble_id = 13, inquilino_id = 7, fechaInicio = new DateTime(2020, 4, 1), fechaFinalizacion = new DateTime(2022, 3, 31), montoAlquiler = 90000, estado = false },
            new() { id = 13, inmueble_id = 21, inquilino_id = 8, fechaInicio = new DateTime(2019, 7, 1), fechaFinalizacion = new DateTime(2021, 6, 30), montoAlquiler = 78000, estado = false },
            new() { id = 14, inmueble_id = 24, inquilino_id = 9, fechaInicio = new DateTime(2020, 5, 1), fechaFinalizacion = new DateTime(2022, 4, 30), montoAlquiler = 95000, estado = false }
        };

        modelBuilder.Entity<Contrato>().HasData(contratos);
    }

    private static void SeedPagos(ModelBuilder modelBuilder)
    {
        var pagos = new List<Pago>
        {
            // Contrato 1 (vigente)
            new() { id = 1, contrato_id = 1, detalle = "Cuota enero 2024", fechaPago = new DateTime(2024, 1, 5), monto = 10000, estado = true },
            new() { id = 2, contrato_id = 1, detalle = "Cuota febrero 2024", fechaPago = new DateTime(2024, 2, 5), monto = 10000, estado = true },
            new() { id = 3, contrato_id = 1, detalle = "Cuota marzo 2024", fechaPago = new DateTime(2024, 3, 5), monto = 10000, estado = true },
            new() { id = 4, contrato_id = 1, detalle = "Cuota abril 2024", fechaPago = new DateTime(2024, 4, 5), monto = 10000, estado = true },
            new() { id = 5, contrato_id = 1, detalle = "Cuota mayo 2024", fechaPago = new DateTime(2024, 5, 5), monto = 10000, estado = true },
            new() { id = 6, contrato_id = 1, detalle = "Cuota junio 2024", fechaPago = new DateTime(2024, 6, 5), monto = 10000, estado = true },

            // Contrato 2 (vigente)
            new() { id = 7, contrato_id = 2, detalle = "Cuota mayo 2023", fechaPago = new DateTime(2023, 5, 10), monto = 9500, estado = true },
            new() { id = 8, contrato_id = 2, detalle = "Cuota junio 2023", fechaPago = new DateTime(2023, 6, 10), monto = 9500, estado = true },
            new() { id = 9, contrato_id = 2, detalle = "Cuota julio 2023", fechaPago = new DateTime(2023, 7, 10), monto = 9500, estado = true },
            new() { id = 10, contrato_id = 2, detalle = "Cuota agosto 2023", fechaPago = new DateTime(2023, 8, 10), monto = 9500, estado = true },
            new() { id = 11, contrato_id = 2, detalle = "Cuota septiembre 2023", fechaPago = new DateTime(2023, 9, 10), monto = 9500, estado = true },

            // Contrato 3 (vigente)
            new() { id = 12, contrato_id = 3, detalle = "Cuota julio 2024", fechaPago = new DateTime(2024, 7, 8), monto = 14000, estado = true },
            new() { id = 13, contrato_id = 3, detalle = "Cuota agosto 2024", fechaPago = new DateTime(2024, 8, 8), monto = 14000, estado = true },
            new() { id = 14, contrato_id = 3, detalle = "Cuota septiembre 2024", fechaPago = new DateTime(2024, 9, 8), monto = 14000, estado = true },

            // Contrato 4 (vigente)
            new() { id = 15, contrato_id = 4, detalle = "Cuota noviembre 2023", fechaPago = new DateTime(2023, 11, 15), monto = 9900, estado = true },
            new() { id = 16, contrato_id = 4, detalle = "Cuota diciembre 2023", fechaPago = new DateTime(2023, 12, 15), monto = 9900, estado = true },
            new() { id = 17, contrato_id = 4, detalle = "Cuota enero 2024", fechaPago = new DateTime(2024, 1, 15), monto = 9900, estado = true },
            new() { id = 18, contrato_id = 4, detalle = "Cuota febrero 2024", fechaPago = new DateTime(2024, 2, 15), monto = 9900, estado = true },
            new() { id = 19, contrato_id = 4, detalle = "Cuota marzo 2024", fechaPago = new DateTime(2024, 3, 15), monto = 9900, estado = true },
            new() { id = 20, contrato_id = 4, detalle = "Cuota abril 2024", fechaPago = new DateTime(2024, 4, 15), monto = 9900, estado = true },
            new() { id = 21, contrato_id = 4, detalle = "Cuota mayo 2024", fechaPago = new DateTime(2024, 5, 15), monto = 9900, estado = true },
            new() { id = 22, contrato_id = 4, detalle = "Cuota junio 2024", fechaPago = new DateTime(2024, 6, 15), monto = 9900, estado = true },

            // Contrato 5 (vigente)
            new() { id = 23, contrato_id = 5, detalle = "Cuota febrero 2024", fechaPago = new DateTime(2024, 2, 12), monto = 15500, estado = true },
            new() { id = 24, contrato_id = 5, detalle = "Cuota marzo 2024", fechaPago = new DateTime(2024, 3, 12), monto = 15500, estado = true },
            new() { id = 25, contrato_id = 5, detalle = "Cuota abril 2024", fechaPago = new DateTime(2024, 4, 12), monto = 15500, estado = true },
            new() { id = 26, contrato_id = 5, detalle = "Cuota mayo 2024", fechaPago = new DateTime(2024, 5, 12), monto = 15500, estado = true },

            // Contrato 6 (vigente)
            new() { id = 27, contrato_id = 6, detalle = "Cuota marzo 2024", fechaPago = new DateTime(2024, 3, 18), monto = 7800, estado = true },
            new() { id = 28, contrato_id = 6, detalle = "Cuota abril 2024", fechaPago = new DateTime(2024, 4, 18), monto = 7800, estado = true },
            new() { id = 29, contrato_id = 6, detalle = "Cuota mayo 2024", fechaPago = new DateTime(2024, 5, 18), monto = 7800, estado = true },
            new() { id = 30, contrato_id = 6, detalle = "Cuota junio 2024", fechaPago = new DateTime(2024, 6, 18), monto = 7800, estado = true },
            new() { id = 31, contrato_id = 6, detalle = "Cuota julio 2024", fechaPago = new DateTime(2024, 7, 18), monto = 7800, estado = true },

            // Contrato 7 (vigente)
            new() { id = 32, contrato_id = 7, detalle = "Cuota mayo 2024", fechaPago = new DateTime(2024, 5, 20), monto = 13200, estado = true },
            new() { id = 33, contrato_id = 7, detalle = "Cuota junio 2024", fechaPago = new DateTime(2024, 6, 20), monto = 13200, estado = true },
            new() { id = 34, contrato_id = 7, detalle = "Cuota julio 2024", fechaPago = new DateTime(2024, 7, 20), monto = 13200, estado = true },
            new() { id = 35, contrato_id = 7, detalle = "Cuota agosto 2024", fechaPago = new DateTime(2024, 8, 20), monto = 13200, estado = true },
            new() { id = 36, contrato_id = 7, detalle = "Cuota septiembre 2024", fechaPago = new DateTime(2024, 9, 20), monto = 13200, estado = true },
            new() { id = 37, contrato_id = 7, detalle = "Cuota octubre 2024", fechaPago = new DateTime(2024, 10, 20), monto = 13200, estado = true },

            // Contrato 8 (vencido)
            new() { id = 38, contrato_id = 8, detalle = "Cuota enero 2020", fechaPago = new DateTime(2020, 1, 5), monto = 5500, estado = true },
            new() { id = 39, contrato_id = 8, detalle = "Cuota febrero 2020", fechaPago = new DateTime(2020, 2, 5), monto = 5500, estado = true },
            new() { id = 40, contrato_id = 8, detalle = "Cuota marzo 2020", fechaPago = new DateTime(2020, 3, 5), monto = 5500, estado = true },
            new() { id = 41, contrato_id = 8, detalle = "Cuota abril 2020", fechaPago = new DateTime(2020, 4, 5), monto = 5500, estado = true },
            new() { id = 42, contrato_id = 8, detalle = "Cuota mayo 2020", fechaPago = new DateTime(2020, 5, 5), monto = 5500, estado = true },
            new() { id = 43, contrato_id = 8, detalle = "Cuota junio 2020", fechaPago = new DateTime(2020, 6, 5), monto = 5500, estado = true },
            new() { id = 44, contrato_id = 8, detalle = "Cuota julio 2020", fechaPago = new DateTime(2020, 7, 5), monto = 5500, estado = true },
            new() { id = 45, contrato_id = 8, detalle = "Cuota agosto 2020", fechaPago = new DateTime(2020, 8, 5), monto = 5500, estado = true },
            new() { id = 46, contrato_id = 8, detalle = "Cuota septiembre 2020", fechaPago = new DateTime(2020, 9, 5), monto = 5500, estado = true },
            new() { id = 47, contrato_id = 8, detalle = "Cuota octubre 2020", fechaPago = new DateTime(2020, 10, 5), monto = 5500, estado = true },

            // Contrato 9 (vencido)
            new() { id = 48, contrato_id = 9, detalle = "Cuota junio 2019", fechaPago = new DateTime(2019, 6, 10), monto = 6800, estado = true },
            new() { id = 49, contrato_id = 9, detalle = "Cuota julio 2019", fechaPago = new DateTime(2019, 7, 10), monto = 6800, estado = true },
            new() { id = 50, contrato_id = 9, detalle = "Cuota agosto 2019", fechaPago = new DateTime(2019, 8, 10), monto = 6800, estado = true },
            new() { id = 51, contrato_id = 9, detalle = "Cuota septiembre 2019", fechaPago = new DateTime(2019, 9, 10), monto = 6800, estado = true },
            new() { id = 52, contrato_id = 9, detalle = "Cuota octubre 2019", fechaPago = new DateTime(2019, 10, 10), monto = 6800, estado = true },
            new() { id = 53, contrato_id = 9, detalle = "Cuota noviembre 2019", fechaPago = new DateTime(2019, 11, 10), monto = 6800, estado = true },
            new() { id = 54, contrato_id = 9, detalle = "Cuota diciembre 2019", fechaPago = new DateTime(2019, 12, 10), monto = 6800, estado = true },
            new() { id = 55, contrato_id = 9, detalle = "Cuota enero 2020", fechaPago = new DateTime(2020, 1, 10), monto = 6800, estado = true },
            new() { id = 56, contrato_id = 9, detalle = "Cuota febrero 2020", fechaPago = new DateTime(2020, 2, 10), monto = 6800, estado = true },

            // Contrato 10 (vencido)
            new() { id = 57, contrato_id = 10, detalle = "Cuota marzo 2018", fechaPago = new DateTime(2018, 3, 12), monto = 6300, estado = true },
            new() { id = 58, contrato_id = 10, detalle = "Cuota abril 2018", fechaPago = new DateTime(2018, 4, 12), monto = 6300, estado = true },
            new() { id = 59, contrato_id = 10, detalle = "Cuota mayo 2018", fechaPago = new DateTime(2018, 5, 12), monto = 6300, estado = true },
            new() { id = 60, contrato_id = 10, detalle = "Cuota junio 2018", fechaPago = new DateTime(2018, 6, 12), monto = 6300, estado = true },
            new() { id = 61, contrato_id = 10, detalle = "Cuota julio 2018", fechaPago = new DateTime(2018, 7, 12), monto = 6300, estado = true },
            new() { id = 62, contrato_id = 10, detalle = "Cuota agosto 2018", fechaPago = new DateTime(2018, 8, 12), monto = 6300, estado = true },
            new() { id = 63, contrato_id = 10, detalle = "Cuota septiembre 2018", fechaPago = new DateTime(2018, 9, 12), monto = 6300, estado = true },

            // Contrato 11 (vencido)
            new() { id = 64, contrato_id = 11, detalle = "Cuota septiembre 2019", fechaPago = new DateTime(2019, 9, 18), monto = 8000, estado = true },
            new() { id = 65, contrato_id = 11, detalle = "Cuota octubre 2019", fechaPago = new DateTime(2019, 10, 18), monto = 8000, estado = true },
            new() { id = 66, contrato_id = 11, detalle = "Cuota noviembre 2019", fechaPago = new DateTime(2019, 11, 18), monto = 8000, estado = true },
            new() { id = 67, contrato_id = 11, detalle = "Cuota diciembre 2019", fechaPago = new DateTime(2019, 12, 18), monto = 8000, estado = true },
            new() { id = 68, contrato_id = 11, detalle = "Cuota enero 2020", fechaPago = new DateTime(2020, 1, 18), monto = 8000, estado = true },
            new() { id = 69, contrato_id = 11, detalle = "Cuota febrero 2020", fechaPago = new DateTime(2020, 2, 18), monto = 8000, estado = true },

            // Contrato 12 (vencido)
            new() { id = 70, contrato_id = 12, detalle = "Cuota abril 2020", fechaPago = new DateTime(2020, 4, 22), monto = 9000, estado = true },
            new() { id = 71, contrato_id = 12, detalle = "Cuota mayo 2020", fechaPago = new DateTime(2020, 5, 22), monto = 9000, estado = true },
            new() { id = 72, contrato_id = 12, detalle = "Cuota junio 2020", fechaPago = new DateTime(2020, 6, 22), monto = 9000, estado = true },
            new() { id = 73, contrato_id = 12, detalle = "Cuota julio 2020", fechaPago = new DateTime(2020, 7, 22), monto = 9000, estado = true },
            new() { id = 74, contrato_id = 12, detalle = "Cuota agosto 2020", fechaPago = new DateTime(2020, 8, 22), monto = 9000, estado = true },
            new() { id = 75, contrato_id = 12, detalle = "Cuota septiembre 2020", fechaPago = new DateTime(2020, 9, 22), monto = 9000, estado = true },
            new() { id = 76, contrato_id = 12, detalle = "Cuota octubre 2020", fechaPago = new DateTime(2020, 10, 22), monto = 9000, estado = true },
            new() { id = 77, contrato_id = 12, detalle = "Cuota noviembre 2020", fechaPago = new DateTime(2020, 11, 22), monto = 9000, estado = true },
            new() { id = 78, contrato_id = 12, detalle = "Cuota diciembre 2020", fechaPago = new DateTime(2020, 12, 22), monto = 9000, estado = true },
            new() { id = 79, contrato_id = 12, detalle = "Cuota enero 2021", fechaPago = new DateTime(2021, 1, 22), monto = 9000, estado = true },

            // Contrato 13 (vencido)
            new() { id = 80, contrato_id = 13, detalle = "Cuota julio 2019", fechaPago = new DateTime(2019, 7, 28), monto = 7800, estado = true },
            new() { id = 81, contrato_id = 13, detalle = "Cuota agosto 2019", fechaPago = new DateTime(2019, 8, 28), monto = 7800, estado = true },
            new() { id = 82, contrato_id = 13, detalle = "Cuota septiembre 2019", fechaPago = new DateTime(2019, 9, 28), monto = 7800, estado = true },
            new() { id = 83, contrato_id = 13, detalle = "Cuota octubre 2019", fechaPago = new DateTime(2019, 10, 28), monto = 7800, estado = true },
            new() { id = 84, contrato_id = 13, detalle = "Cuota noviembre 2019", fechaPago = new DateTime(2019, 11, 28), monto = 7800, estado = true },
            new() { id = 85, contrato_id = 13, detalle = "Cuota diciembre 2019", fechaPago = new DateTime(2019, 12, 28), monto = 7800, estado = true },
            new() { id = 86, contrato_id = 13, detalle = "Cuota enero 2020", fechaPago = new DateTime(2020, 1, 28), monto = 7800, estado = true },
            new() { id = 87, contrato_id = 13, detalle = "Cuota febrero 2020", fechaPago = new DateTime(2020, 2, 28), monto = 7800, estado = true },

            // Contrato 14 (vencido)
            new() { id = 88, contrato_id = 14, detalle = "Cuota mayo 2020", fechaPago = new DateTime(2020, 5, 18), monto = 9500, estado = true },
            new() { id = 89, contrato_id = 14, detalle = "Cuota junio 2020", fechaPago = new DateTime(2020, 6, 18), monto = 9500, estado = true },
            new() { id = 90, contrato_id = 14, detalle = "Cuota julio 2020", fechaPago = new DateTime(2020, 7, 18), monto = 9500, estado = true },
            new() { id = 91, contrato_id = 14, detalle = "Cuota agosto 2020", fechaPago = new DateTime(2020, 8, 18), monto = 9500, estado = true },
            new() { id = 92, contrato_id = 14, detalle = "Cuota septiembre 2020", fechaPago = new DateTime(2020, 9, 18), monto = 9500, estado = true }
        };

        modelBuilder.Entity<Pago>().HasData(pagos);
    }


}