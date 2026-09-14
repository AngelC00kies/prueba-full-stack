using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

/// <summary>
/// Define las operaciones de acceso a datos para la entidad Venta.
/// Esta interfaz establece el contrato para la gestión y persistencia
/// de las ventas realizadas dentro del sistema.
/// </summary>
public interface IVentaRepository
{
    /// <summary>
    /// Obtiene todas las ventas registradas en el sistema.
    /// Puede incluir información relacionada como el cliente
    /// y los detalles de cada venta según la implementación.
    /// </summary>
    /// <returns>
    /// Una colección de ventas registradas.
    /// </returns>
    Task<IEnumerable<Venta>> ObtenerTodasAsync();

    /// <summary>
    /// Obtiene una venta específica utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único de la venta.
    /// </param>
    /// <returns>
    /// La venta encontrada o null si no existe.
    /// </returns>
    Task<Venta?> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Registra una nueva venta en el sistema.
    /// Incluye la información general de la transacción y
    /// los detalles de los productos vendidos.
    /// </summary>
    /// <param name="venta">
    /// Entidad venta que será almacenada.
    /// </param>
    /// <returns>
    /// La venta creada con sus datos actualizados.
    /// </returns>
    Task<Venta> CrearAsync(Venta venta);
}