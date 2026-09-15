using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Infrastructure.Data;

/// <summary>
/// Contexto de Entity Framework Core para la base de datos GestionVentasDB.
/// Configura el mapeo entre las entidades del dominio y las tablas SQL Server.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<DetalleVenta> DetalleVentas => Set<DetalleVenta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==================== USUARIO ====================
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
            entity.Property(u => u.PasswordHash).HasColumnName("passwordhash").HasMaxLength(200).IsRequired();
            entity.Property(u => u.Rol).HasColumnName("role").HasMaxLength(20).HasDefaultValue("user");
            entity.Property(u => u.Activo).HasColumnName("activo").HasDefaultValue(true);
            entity.Property(u => u.FechaCreacion).HasColumnName("fechacreacion").HasDefaultValueSql("getdate()");

            entity.HasIndex(u => u.Username).IsUnique().HasDatabaseName("UQ_Usuario_Username");
        });

        // ==================== CLIENTE ====================
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("cliente");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            entity.Property(c => c.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            entity.Property(c => c.Telefono).HasColumnName("telefono").HasMaxLength(20).IsRequired();
        });

        // ==================== PRODUCTO ====================
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("producto");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Nombre).HasColumnName("nombre").HasMaxLength(30).IsRequired();
            entity.Property(p => p.Descripcion).HasColumnName("descripcion").HasMaxLength(250).IsRequired();
            entity.Property(p => p.Precio).HasColumnName("precio").HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(p => p.Stock).HasColumnName("stock").IsRequired();
        });

        // ==================== VENTA ====================
        modelBuilder.Entity<Venta>(entity =>
        {
            entity.ToTable("ventas");
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Id).HasColumnName("id");
            entity.Property(v => v.Fecha).HasColumnName("fecha").HasDefaultValueSql("getdate()");
            entity.Property(v => v.IdCliente).HasColumnName("idcliente").IsRequired();
            entity.Property(v => v.Total).HasColumnName("total").HasColumnType("decimal(18,2)").IsRequired();

            entity.HasOne(v => v.Cliente)
                  .WithMany(c => c.Ventas)
                  .HasForeignKey(v => v.IdCliente)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ==================== DETALLE VENTA ====================
        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.ToTable("detalleventas");
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).HasColumnName("id");
            entity.Property(d => d.IdVenta).HasColumnName("idventa").IsRequired();
            entity.Property(d => d.IdProducto).HasColumnName("idproducto").IsRequired();
            entity.Property(d => d.Cantidad).HasColumnName("cantidad").IsRequired();
            entity.Property(d => d.PrecioUnitario).HasColumnName("preciounitario").HasColumnType("decimal(18,2)").IsRequired();

            entity.HasOne(d => d.Venta)
                  .WithMany(v => v.Detalles)
                  .HasForeignKey(d => d.IdVenta)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.Detalles)
                  .HasForeignKey(d => d.IdProducto)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}