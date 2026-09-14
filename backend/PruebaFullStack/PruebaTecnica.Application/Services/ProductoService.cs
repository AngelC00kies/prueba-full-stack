using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Productos;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;

    public ProductoService(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ProductoDto>>> ObtenerTodosAsync()
    {
        var productos = await _repository.ObtenerTodosAsync();
        var dtos = productos.Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio,
            Stock = p.Stock
        });
        return Result<IEnumerable<ProductoDto>>.Ok(dtos);
    }

    public async Task<Result<ProductoDto>> ObtenerPorIdAsync(int id)
    {
        var producto = await _repository.ObtenerPorIdAsync(id);
        if (producto is null)
            return Result<ProductoDto>.Fail($"Producto con id {id} no encontrado");

        return Result<ProductoDto>.Ok(new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock
        });
    }

    public async Task<Result<ProductoDto>> CrearAsync(CrearProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Stock = dto.Stock
        };

        var creado = await _repository.CrearAsync(producto);

        return Result<ProductoDto>.Ok(new ProductoDto
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Descripcion = creado.Descripcion,
            Precio = creado.Precio,
            Stock = creado.Stock
        }, "Producto creado exitosamente");
    }

    public async Task<Result<ProductoDto>> ActualizarAsync(int id, ActualizarProductoDto dto)
    {
        var producto = await _repository.ObtenerPorIdAsync(id);
        if (producto is null)
            return Result<ProductoDto>.Fail($"Producto con id {id} no encontrado");

        producto.Nombre = dto.Nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.Stock = dto.Stock;

        var actualizado = await _repository.ActualizarAsync(producto);

        return Result<ProductoDto>.Ok(new ProductoDto
        {
            Id = actualizado.Id,
            Nombre = actualizado.Nombre,
            Descripcion = actualizado.Descripcion,
            Precio = actualizado.Precio,
            Stock = actualizado.Stock
        }, "Producto actualizado exitosamente");
    }

    public async Task<Result<bool>> EliminarAsync(int id)
    {
        var existe = await _repository.ExisteAsync(id);
        if (!existe)
            return Result<bool>.Fail($"Producto con id {id} no encontrado");

        await _repository.EliminarAsync(id);
        return Result<bool>.Ok(true, "Producto eliminado exitosamente");
    }
}