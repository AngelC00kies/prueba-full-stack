using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Infrastructure.Data;

namespace PruebaTecnica.Infrastructure.Repositories;

/// <summary>
/// Repositorio para operaciones CRUD de clientes.
/// </summary>
public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        => await _context.Clientes.AsNoTracking().ToListAsync();

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
        => await _context.Clientes.FindAsync(id);

    public async Task<Cliente> CrearAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> ActualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente is null) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
        => await _context.Clientes.AnyAsync(c => c.Id == id);
}