using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObtenerTodosAsync();
    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task<Cliente> CrearAsync(Cliente cliente);
    Task<Cliente> ActualizarAsync(Cliente cliente);
    Task<bool> EliminarAsync(int id);
    Task<bool> ExisteAsync(int id);
}