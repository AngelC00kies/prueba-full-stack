using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Productos;

namespace PruebaTecnica.Application.Interfaces;

public interface IProductoService
{
    Task<Result<IEnumerable<ProductoDto>>> ObtenerTodosAsync();
    Task<Result<ProductoDto>> ObtenerPorIdAsync(int id);
    Task<Result<ProductoDto>> CrearAsync(CrearProductoDto dto);
    Task<Result<ProductoDto>> ActualizarAsync(int id, ActualizarProductoDto dto);
    Task<Result<bool>> EliminarAsync(int id);
}
