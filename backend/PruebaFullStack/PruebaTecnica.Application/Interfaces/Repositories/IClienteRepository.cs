using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

/// <summary>
/// Define las operaciones de acceso a datos para la entidad Cliente.
/// Esta interfaz establece el contrato que deben implementar los
/// repositorios encargados de la persistencia de clientes.
/// </summary>
public interface IClienteRepository
{
    /// <summary>
    /// Obtiene la lista completa de clientes registrados en el sistema.
    /// </summary>
    /// <returns>
    /// Una colección de clientes.
    /// </returns>
    Task<IEnumerable<Cliente>> ObtenerTodosAsync();

    /// <summary>
    /// Obtiene un cliente específico a partir de su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único del cliente.
    /// </param>
    /// <returns>
    /// El cliente encontrado o null si no existe.
    /// </returns>
    Task<Cliente?> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Crea un nuevo cliente en el sistema.
    /// </summary>
    /// <param name="cliente">
    /// Entidad cliente que será almacenada.
    /// </param>
    /// <returns>
    /// El cliente creado con sus datos actualizados.
    /// </returns>
    Task<Cliente> CrearAsync(Cliente cliente);

    /// <summary>
    /// Actualiza la información de un cliente existente.
    /// </summary>
    /// <param name="cliente">
    /// Entidad cliente con los datos modificados.
    /// </param>
    /// <returns>
    /// El cliente actualizado.
    /// </returns>
    Task<Cliente> ActualizarAsync(Cliente cliente);

    /// <summary>
    /// Elimina un cliente utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador del cliente que será eliminado.
    /// </param>
    /// <returns>
    /// True si la eliminación se realizó correctamente;
    /// False si el cliente no existe o no pudo eliminarse.
    /// </returns>
    Task<bool> EliminarAsync(int id);

    /// <summary>
    /// Verifica si existe un cliente con el identificador especificado.
    /// </summary>
    /// <param name="id">
    /// Identificador del cliente a validar.
    /// </param>
    /// <returns>
    /// True si el cliente existe; False en caso contrario.
    /// </returns>
    Task<bool> ExisteAsync(int id);
}