using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Clientes;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ClienteDto>>> ObtenerTodosAsync()
    {
        var clientes = await _repository.ObtenerTodosAsync();
        var dtos = clientes.Select(c => new ClienteDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono
        });
        return Result<IEnumerable<ClienteDto>>.Ok(dtos);
    }

    public async Task<Result<ClienteDto>> ObtenerPorIdAsync(int id)
    {
        var cliente = await _repository.ObtenerPorIdAsync(id);
        if (cliente is null)
            return Result<ClienteDto>.Fail($"Cliente con id {id} no encontrado");

        return Result<ClienteDto>.Ok(new ClienteDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        });
    }

    public async Task<Result<ClienteDto>> CrearAsync(CrearClienteDto dto)
    {
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Telefono = dto.Telefono
        };

        var creado = await _repository.CrearAsync(cliente);

        return Result<ClienteDto>.Ok(new ClienteDto
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Email = creado.Email,
            Telefono = creado.Telefono
        }, "Cliente creado exitosamente");
    }

    public async Task<Result<ClienteDto>> ActualizarAsync(int id, ActualizarClienteDto dto)
    {
        var cliente = await _repository.ObtenerPorIdAsync(id);
        if (cliente is null)
            return Result<ClienteDto>.Fail($"Cliente con id {id} no encontrado");

        cliente.Nombre = dto.Nombre;
        cliente.Email = dto.Email;
        cliente.Telefono = dto.Telefono;

        var actualizado = await _repository.ActualizarAsync(cliente);

        return Result<ClienteDto>.Ok(new ClienteDto
        {
            Id = actualizado.Id,
            Nombre = actualizado.Nombre,
            Email = actualizado.Email,
            Telefono = actualizado.Telefono
        }, "Cliente actualizado exitosamente");
    }

    public async Task<Result<bool>> EliminarAsync(int id)
    {
        var existe = await _repository.ExisteAsync(id);
        if (!existe)
            return Result<bool>.Fail($"Cliente con id {id} no encontrado");

        await _repository.EliminarAsync(id);
        return Result<bool>.Ok(true, "Cliente eliminado exitosamente");
    }
}
