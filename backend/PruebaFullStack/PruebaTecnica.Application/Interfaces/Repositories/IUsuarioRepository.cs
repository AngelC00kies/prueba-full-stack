using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario> ObtenerPorUsernameAsync(string username);
    Task<Usuario> CrearAsync(Usuario usuario);
    Task<bool> ExisteUsernameAsync(string username);
}
