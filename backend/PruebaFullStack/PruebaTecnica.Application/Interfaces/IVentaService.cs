using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Ventas;

namespace PruebaTecnica.Application.Interfaces;

/// <summary>
/// Define los servicios de aplicación para la gestión de ventas.
/// Esta interfaz establece las operaciones relacionadas con la consulta
/// y el registro de ventas dentro del sistema.
/// </summary>
public interface IVentaService
{
    /// <summary>
    /// Obtiene todas las ventas registradas en el sistema.
    /// Incluye la información general de cada venta y sus detalles asociados.
    /// </summary>
    /// <returns>
    /// Un resultado que contiene la colección de ventas registradas.
    /// </returns>
    Task<Result<IEnumerable<VentaDto>>> ObtenerTodasAsync();

    /// <summary>
    /// Obtiene una venta específica mediante su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único de la venta.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información de la venta encontrada.
    /// </returns>
    Task<Result<VentaDto>> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Registra una nueva venta en el sistema.
    /// Procesa la información recibida, valida los datos necesarios
    /// y genera la transacción correspondiente.
    /// </summary>
    /// <param name="dto">
    /// Datos requeridos para la creación de la venta.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información de la venta creada.
    /// </returns>
    Task<Result<VentaDto>> CrearAsync(CrearVentaDto dto);
}