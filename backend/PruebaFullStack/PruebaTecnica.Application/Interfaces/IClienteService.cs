using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Clientes;

namespace PruebaTecnica.Application.Interfaces;

public interface IClienteService
{
    Task<Result<IEnumerable<ClienteDto>>> ObtenerTodosAsync();
    Task<Result<ClienteDto>> ObtenerPorIdAsync(int id);
    Task<Result<ClienteDto>> CrearAsync(CrearClienteDto dto);
    Task<Result<ClienteDto>> ActualizarAsync(int id, ActualizarClienteDto dto);
    Task<Result<bool>> EliminarAsync(int id);
}