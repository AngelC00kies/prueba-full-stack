using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Infrastructure.Data;

namespace PruebaTecnica.Infrastructure.Repositories;

/// <summary>
/// Repositorio para la gestión de ventas y sus detalles.
/// </summary>
public class VentaRepository : IVentaRepository
{
    private readonly ApplicationDbContext _context;

    public VentaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venta>> ObtenerTodasAsync()
        => await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Venta?> ObtenerPorIdAsync(int id)
        => await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.Id == id);

    public async Task<Venta> CrearAsync(Venta venta)
    {
        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();
        return venta;
    }
}