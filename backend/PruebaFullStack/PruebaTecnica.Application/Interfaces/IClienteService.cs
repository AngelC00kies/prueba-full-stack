using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Clientes;

namespace PruebaTecnica.Application.Interfaces;

/// <summary>
/// Define los servicios de aplicación para la gestión de clientes.
/// Esta interfaz establece las operaciones disponibles para consultar,
/// crear, actualizar y eliminar clientes dentro del sistema.
/// </summary>
public interface IClienteService
{
    /// <summary>
    /// Obtiene todos los clientes registrados.
    /// </summary>
    /// <returns>
    /// Un resultado que contiene la colección de clientes disponibles.
    /// </returns>
    Task<Result<IEnumerable<ClienteDto>>> ObtenerTodosAsync();

    /// <summary>
    /// Obtiene un cliente específico mediante su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único del cliente.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información del cliente encontrado.
    /// </returns>
    Task<Result<ClienteDto>> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Registra un nuevo cliente en el sistema.
    /// </summary>
    /// <param name="dto">
    /// Datos necesarios para la creación del cliente.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información del cliente creado.
    /// </returns>
    Task<Result<ClienteDto>> CrearAsync(CrearClienteDto dto);

    /// <summary>
    /// Actualiza la información de un cliente existente.
    /// </summary>
    /// <param name="id">
    /// Identificador del cliente que será actualizado.
    /// </param>
    /// <param name="dto">
    /// Datos actualizados del cliente.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información actualizada del cliente.
    /// </returns>
    Task<Result<ClienteDto>> ActualizarAsync(int id, ActualizarClienteDto dto);

    /// <summary>
    /// Elimina un cliente del sistema utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador del cliente que será eliminado.
    /// </param>
    /// <returns>
    /// Un resultado que indica si la operación se realizó correctamente.
    /// </returns>
    Task<Result<bool>> EliminarAsync(int id);
}