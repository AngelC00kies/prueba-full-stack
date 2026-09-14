using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Productos;

namespace PruebaTecnica.Application.Interfaces;

/// <summary>
/// Define los servicios de aplicación para la gestión de productos.
/// Esta interfaz establece las operaciones disponibles para consultar,
/// crear, actualizar y eliminar productos dentro del sistema.
/// </summary>
public interface IProductoService
{
    /// <summary>
    /// Obtiene todos los productos registrados.
    /// </summary>
    /// <returns>
    /// Un resultado que contiene la colección de productos disponibles.
    /// </returns>
    Task<Result<IEnumerable<ProductoDto>>> ObtenerTodosAsync();

    /// <summary>
    /// Obtiene un producto específico mediante su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único del producto.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información del producto encontrado.
    /// </returns>
    Task<Result<ProductoDto>> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Registra un nuevo producto en el sistema.
    /// </summary>
    /// <param name="dto">
    /// Datos necesarios para la creación del producto.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información del producto creado.
    /// </returns>
    Task<Result<ProductoDto>> CrearAsync(CrearProductoDto dto);

    /// <summary>
    /// Actualiza la información de un producto existente.
    /// </summary>
    /// <param name="id">
    /// Identificador del producto que será actualizado.
    /// </param>
    /// <param name="dto">
    /// Datos actualizados del producto.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información actualizada del producto.
    /// </returns>
    Task<Result<ProductoDto>> ActualizarAsync(int id, ActualizarProductoDto dto);

    /// <summary>
    /// Elimina un producto del sistema utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador del producto que será eliminado.
    /// </param>
    /// <returns>
    /// Un resultado que indica si la operación se realizó correctamente.
    /// </returns>
    Task<Result<bool>> EliminarAsync(int id);
}