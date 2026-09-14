using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Ventas;

namespace PruebaTecnica.Application.Interfaces;

public interface IVentaService
{
    Task<Result<IEnumerable<VentaDto>>> ObtenerTodasAsync();
    Task<Result<VentaDto>> ObtenerPorIdAsync(int id);
    Task<Result<VentaDto>> CrearAsync(CrearVentaDto dto);
}