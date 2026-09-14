using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Clientes;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

/// <summary>
/// Servicio encargado de gestionar las operaciones relacionadas
/// con los clientes, incluyendo consultas, creación,
/// actualización y eliminación de registros.
/// </summary>
public class ClienteService : IClienteService
{
    /// <summary>
    /// Repositorio utilizado para el acceso y manipulación
    /// de los datos de clientes.
    /// </summary>
    private readonly IClienteRepository _repository;

    /// <summary>
    /// Inicializa una nueva instancia del servicio de clientes.
    /// </summary>
    /// <param name="repository">
    /// Repositorio encargado de la persistencia de clientes.
    /// </param>
    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene la lista completa de clientes registrados
    /// en el sistema y los transforma a objetos DTO.
    /// </summary>
    /// <returns>
    /// Resultado que contiene la colección de clientes.
    /// </returns>
    public async Task<Result<IEnumerable<ClienteDto>>> ObtenerTodosAsync()
    {
        // Obtiene todos los clientes desde el repositorio.
        var clientes = await _repository.ObtenerTodosAsync();

        // Convierte las entidades de dominio a DTOs.
        var dtos = clientes.Select(c => new ClienteDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono
        });

        return Result<IEnumerable<ClienteDto>>.Ok(dtos);
    }

    /// <summary>
    /// Obtiene un cliente específico utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único del cliente.
    /// </param>
    /// <returns>
    /// Resultado con la información del cliente encontrado
    /// o un mensaje de error si no existe.
    /// </returns>
    public async Task<Result<ClienteDto>> ObtenerPorIdAsync(int id)
    {
        // Busca el cliente por su identificador.
        var cliente = await _repository.ObtenerPorIdAsync(id);

        // Verifica si el cliente existe.
        if (cliente is null)
            return Result<ClienteDto>.Fail($"Cliente con id {id} no encontrado");

        // Convierte la entidad a DTO para su devolución.
        return Result<ClienteDto>.Ok(new ClienteDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        });
    }

    /// <summary>
    /// Registra un nuevo cliente en el sistema.
    /// </summary>
    /// <param name="dto">
    /// Información necesaria para crear el cliente.
    /// </param>
    /// <returns>
    /// Resultado con la información del cliente creado.
    /// </returns>
    public async Task<Result<ClienteDto>> CrearAsync(CrearClienteDto dto)
    {
        // Crea una nueva entidad Cliente a partir de los datos recibidos.
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Telefono = dto.Telefono
        };

        // Guarda el cliente en la base de datos.
        var creado = await _repository.CrearAsync(cliente);

        // Retorna el cliente creado convertido a DTO.
        return Result<ClienteDto>.Ok(new ClienteDto
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Email = creado.Email,
            Telefono = creado.Telefono
        }, "Cliente creado exitosamente");
    }

    /// <summary>
    /// Actualiza la información de un cliente existente.
    /// </summary>
    /// <param name="id">
    /// Identificador del cliente a actualizar.
    /// </param>
    /// <param name="dto">
    /// Datos actualizados del cliente.
    /// </param>
    /// <returns>
    /// Resultado con la información actualizada del cliente.
    /// </returns>
    public async Task<Result<ClienteDto>> ActualizarAsync(int id, ActualizarClienteDto dto)
    {
        // Busca el cliente a actualizar.
        var cliente = await _repository.ObtenerPorIdAsync(id);

        // Verifica que el cliente exista.
        if (cliente is null)
            return Result<ClienteDto>.Fail($"Cliente con id {id} no encontrado");

        // Actualiza las propiedades de la entidad.
        cliente.Nombre = dto.Nombre;
        cliente.Email = dto.Email;
        cliente.Telefono = dto.Telefono;

        // Guarda los cambios realizados.
        var actualizado = await _repository.ActualizarAsync(cliente);

        // Retorna el cliente actualizado convertido a DTO.
        return Result<ClienteDto>.Ok(new ClienteDto
        {
            Id = actualizado.Id,
            Nombre = actualizado.Nombre,
            Email = actualizado.Email,
            Telefono = actualizado.Telefono
        }, "Cliente actualizado exitosamente");
    }

    /// <summary>
    /// Elimina un cliente del sistema utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador del cliente que será eliminado.
    /// </param>
    /// <returns>
    /// Resultado indicando si la eliminación fue exitosa.
    /// </returns>
    public async Task<Result<bool>> EliminarAsync(int id)
    {
        // Verifica que el cliente exista antes de intentar eliminarlo.
        var existe = await _repository.ExisteAsync(id);

        if (!existe)
            return Result<bool>.Fail($"Cliente con id {id} no encontrado");

        // Elimina el cliente.
        await _repository.EliminarAsync(id);

        // Retorna una respuesta exitosa.
        return Result<bool>.Ok(true, "Cliente eliminado exitosamente");
    }
}