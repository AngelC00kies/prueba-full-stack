using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> ObtenerTodosAsync();
    Task<Producto?> ObtenerPorIdAsync(int id);
    Task<Producto> CrearAsync(Producto producto);
    Task<Producto> ActualizarAsync(Producto producto);
    Task<bool> EliminarAsync(int id);
    Task<bool> ExisteAsync(int id);
}