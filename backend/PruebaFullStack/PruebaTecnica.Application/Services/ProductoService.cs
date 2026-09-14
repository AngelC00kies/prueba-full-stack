using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Productos;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

/// <summary>
/// Servicio encargado de gestionar las operaciones relacionadas
/// con los productos, incluyendo consultas, creación,
/// actualización y eliminación de registros.
/// </summary>
public class ProductoService : IProductoService
{
    /// <summary>
    /// Repositorio responsable del acceso y manipulación
    /// de la información de los productos.
    /// </summary>
    private readonly IProductoRepository _repository;

    /// <summary>
    /// Inicializa una nueva instancia del servicio de productos.
    /// </summary>
    /// <param name="repository">
    /// Repositorio utilizado para gestionar la persistencia de productos.
    /// </param>
    public ProductoService(IProductoRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene todos los productos registrados en el sistema.
    /// Convierte las entidades de dominio a objetos DTO para su exposición.
    /// </summary>
    /// <returns>
    /// Resultado que contiene la colección de productos.
    /// </returns>
    public async Task<Result<IEnumerable<ProductoDto>>> ObtenerTodosAsync()
    {
        // Obtiene todos los productos desde el repositorio.
        var productos = await _repository.ObtenerTodosAsync();

        // Convierte cada entidad Producto en un DTO.
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

    /// <summary>
    /// Obtiene un producto específico mediante su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador único del producto.
    /// </param>
    /// <returns>
    /// Resultado que contiene la información del producto encontrado
    /// o un mensaje de error si no existe.
    /// </returns>
    public async Task<Result<ProductoDto>> ObtenerPorIdAsync(int id)
    {
        // Busca el producto por su identificador.
        var producto = await _repository.ObtenerPorIdAsync(id);

        // Verifica que el producto exista.
        if (producto is null)
            return Result<ProductoDto>.Fail($"Producto con id {id} no encontrado");

        // Convierte la entidad a DTO para devolverla al consumidor.
        return Result<ProductoDto>.Ok(new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock
        });
    }

    /// <summary>
    /// Registra un nuevo producto en el sistema.
    /// </summary>
    /// <param name="dto">
    /// Datos necesarios para la creación del producto.
    /// </param>
    /// <returns>
    /// Resultado con la información del producto creado.
    /// </returns>
    public async Task<Result<ProductoDto>> CrearAsync(CrearProductoDto dto)
    {
        // Crea una nueva entidad Producto utilizando la información recibida.
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Stock = dto.Stock
        };

        // Guarda el producto en la base de datos.
        var creado = await _repository.CrearAsync(producto);

        // Retorna la información del producto creado.
        return Result<ProductoDto>.Ok(new ProductoDto
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Descripcion = creado.Descripcion,
            Precio = creado.Precio,
            Stock = creado.Stock
        }, "Producto creado exitosamente");
    }

    /// <summary>
    /// Actualiza la información de un producto existente.
    /// </summary>
    /// <param name="id">
    /// Identificador del producto a actualizar.
    /// </param>
    /// <param name="dto">
    /// Datos actualizados del producto.
    /// </param>
    /// <returns>
    /// Resultado con la información actualizada del producto.
    /// </returns>
    public async Task<Result<ProductoDto>> ActualizarAsync(int id, ActualizarProductoDto dto)
    {
        // Busca el producto que será actualizado.
        var producto = await _repository.ObtenerPorIdAsync(id);

        // Verifica que el producto exista.
        if (producto is null)
            return Result<ProductoDto>.Fail($"Producto con id {id} no encontrado");

        // Actualiza las propiedades con los nuevos valores.
        producto.Nombre = dto.Nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.Stock = dto.Stock;

        // Guarda los cambios realizados.
        var actualizado = await _repository.ActualizarAsync(producto);

        // Retorna la información actualizada.
        return Result<ProductoDto>.Ok(new ProductoDto
        {
            Id = actualizado.Id,
            Nombre = actualizado.Nombre,
            Descripcion = actualizado.Descripcion,
            Precio = actualizado.Precio,
            Stock = actualizado.Stock
        }, "Producto actualizado exitosamente");
    }

    /// <summary>
    /// Elimina un producto del sistema utilizando su identificador.
    /// </summary>
    /// <param name="id">
    /// Identificador del producto que será eliminado.
    /// </param>
    /// <returns>
    /// Resultado que indica si la eliminación fue exitosa.
    /// </returns>
    public async Task<Result<bool>> EliminarAsync(int id)
    {
        // Verifica previamente si el producto existe.
        var existe = await _repository.ExisteAsync(id);

        if (!existe)
            return Result<bool>.Fail($"Producto con id {id} no encontrado");

        // Elimina el producto de la base de datos.
        await _repository.EliminarAsync(id);

        // Retorna una respuesta satisfactoria.
        return Result<bool>.Ok(true, "Producto eliminado exitosamente");
    }
}