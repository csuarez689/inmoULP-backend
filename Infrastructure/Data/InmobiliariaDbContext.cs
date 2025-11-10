using Microsoft.EntityFrameworkCore;
using InmobiliariaAPI.Domain.Entities;
using InmobiliariaAPI.Infrastructure.Data.Seeds;

namespace InmobiliariaAPI.Infrastructure.Data;

public class InmobiliariaDbContext : DbContext
{
    public InmobiliariaDbContext(DbContextOptions<InmobiliariaDbContext> options) : base(options)
    {
    }

    public DbSet<Propietario> Propietarios { get; set; }
    public DbSet<Inmueble> Inmuebles { get; set; }
    public DbSet<Inquilino> Inquilinos { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<ImagenInmueble> ImagenesInmuebles { get; set; }
    public DbSet<TipoInmueble> TiposInmueble { get; set; }
    public DbSet<UsoInmueble> UsosInmueble { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Propietario
        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.Property(p => p.dni)
                .IsRequired()
                .HasMaxLength(9);
                
            entity.Property(p => p.nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.apellido)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.telefono)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(p => p.password)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(p => p.activo)
                .HasDefaultValue(true);
            
            entity.HasIndex(p => p.dni).IsUnique();
            entity.HasIndex(p => p.email).IsUnique();
        });

        // Configuración de Inquilino
        modelBuilder.Entity<Inquilino>(entity =>
        {
            entity.Property(i => i.nombre).HasMaxLength(100).IsRequired();
            entity.Property(i => i.apellido).HasMaxLength(100).IsRequired();
            entity.Property(i => i.email).HasMaxLength(255).IsRequired();
            entity.Property(i => i.telefono).HasMaxLength(20).IsRequired();
            
            entity.HasIndex(i => i.dni).IsUnique();
            entity.HasIndex(i => i.email).IsUnique();
        });

        
        // Configuración de Inmueble
        modelBuilder.Entity<Inmueble>(entity =>
        {
            entity.Property(i => i.direccion).HasMaxLength(500);
            entity.Property(i => i.precio).HasColumnType("decimal(18,2)");
            entity.Property(i => i.latitud).HasMaxLength(20);
            entity.Property(i => i.longitud).HasMaxLength(20);
        });

        // Configuración de Contrato
        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.Property(c => c.montoAlquiler).HasColumnType("decimal(18,2)");
        });

        // Configuración de Pago
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.Property(p => p.detalle).HasMaxLength(500).IsRequired();
            entity.Property(p => p.monto).HasColumnType("decimal(18,2)");
        });

        // Configuración de ImagenInmueble
        modelBuilder.Entity<ImagenInmueble>(entity =>
        {
            entity.Property(i => i.url).HasMaxLength(1000).IsRequired();
        });

        // Configuración de TipoInmueble
        modelBuilder.Entity<TipoInmueble>(entity =>
        {
            entity.Property(t => t.nombre).HasMaxLength(100).IsRequired();
        });

        // Configuración de UsoInmueble
        modelBuilder.Entity<UsoInmueble>(entity =>
        {
            entity.Property(u => u.nombre).HasMaxLength(100).IsRequired();
        });


        // Seed data
        DataSeeder.SeedData(modelBuilder);
    }
}
