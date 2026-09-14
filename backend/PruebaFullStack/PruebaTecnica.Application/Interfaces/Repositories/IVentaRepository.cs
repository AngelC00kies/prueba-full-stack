using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

public interface IVentaRepository
{
    Task<IEnumerable<Venta>> ObtenerTodasAsync();
    Task<Venta?> ObtenerPorIdAsync(int id);
    Task<Venta> CrearAsync(Venta venta);
}