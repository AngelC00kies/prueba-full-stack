using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

/// <summary>
/// Define las operaciones de acceso a datos para la entidad Producto.
/// Esta interfaz actúa como un contrato entre la capa de aplicación
/// y la capa de infraestructura para la gestión de productos.
/// </summary>
public interface IProductoRepository
{
    /// <summary>
    /// Obtiene todos los productos registrados en el sistema.
    /// </summary>
    /// <returns>
    /// Una colección de productos disponibles.
    /// </returns>
    Task<IEnumerable<Producto>> ObtenerTodosAsync();

    /// <summary>
    /// Obtiene un producto específico a partir de su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único del producto.
    /// </param>
    /// <returns>
    /// El producto encontrado o null si no existe.
    /// </returns>
    Task<Producto?> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Crea un nuevo producto en el sistema.
    /// </summary>
    /// <param name="producto">
    /// Entidad producto que será almacenada.
    /// </param>
    /// <returns>
    /// El producto creado con sus datos actualizados.
    /// </returns>
    Task<Producto> CrearAsync(Producto producto);

    /// <summary>
    /// Actualiza la información de un producto existente.
    /// </summary>
    /// <param name="producto">
    /// Entidad producto con los datos modificados.
    /// </param>
    /// <returns>
    /// El producto actualizado.
    /// </returns>
    Task<Producto> ActualizarAsync(Producto producto);

    /// <summary>
    /// Elimina un producto utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador del producto que será eliminado.
    /// </param>
    /// <returns>
    /// True si el producto fue eliminado correctamente;
    /// False si no existe o no pudo eliminarse.
    /// </returns>
    Task<bool> EliminarAsync(int id);

    /// <summary>
    /// Verifica si existe un producto con el identificador especificado.
    /// </summary>
    /// <param name="id">
    /// Identador único del producto a validar.
    /// </param>
    /// <returns>
    /// True si el producto existe; False en caso contrario.
    /// </returns>
    Task<bool> ExisteAsync(int id);
}