using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Infrastructure.Data;

namespace PruebaTecnica.Infrastructure.Repositories;

/// <summary>
/// Repositorio para operaciones CRUD de productos.
/// </summary>
public class ProductoRepository : IProductoRepository
{
    private readonly ApplicationDbContext _context;

    public ProductoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        => await _context.Productos.AsNoTracking().ToListAsync();

    public async Task<Producto?> ObtenerPorIdAsync(int id)
        => await _context.Productos.FindAsync(id);

    public async Task<Producto> CrearAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto> ActualizarAsync(Producto producto)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto is null) return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
        => await _context.Productos.AnyAsync(p => p.Id == id);
}