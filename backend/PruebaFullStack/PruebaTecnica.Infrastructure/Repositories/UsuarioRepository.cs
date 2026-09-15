using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Infrastructure.Data;

namespace PruebaTecnica.Infrastructure.Repositories;

/// <summary>
/// Repositorio para la gestión de usuarios del sistema.
/// </summary>
public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObtenerPorUsernameAsync(string username)
        => await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);

    public async Task<Usuario> CrearAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> ExisteUsernameAsync(string username)
        => await _context.Usuarios.AnyAsync(u => u.Username == username);
}