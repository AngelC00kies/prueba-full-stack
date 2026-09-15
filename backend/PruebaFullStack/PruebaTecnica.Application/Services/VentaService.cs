using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Ventas;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

/// <summary>
/// Servicio encargado de la gestión de ventas.
/// Contiene la lógica de negocio relacionada con la consulta
/// y registro de ventas, así como la validación de clientes,
/// productos y control de inventario.
/// </summary>
public class VentaService : IVentaService
{
    /// <summary>
    /// Repositorio utilizado para la persistencia y consulta de ventas.
    /// </summary>
    private readonly IVentaRepository _ventaRepository;

    /// <summary>
    /// Repositorio utilizado para la gestión de productos.
    /// </summary>
    private readonly IProductoRepository _productoRepository;

    /// <summary>
    /// Repositorio utilizado para la gestión de clientes.
    /// </summary>
    private readonly IClienteRepository _clienteRepository;

    /// <summary>
    /// Inicializa una nueva instancia del servicio de ventas.
    /// </summary>
    /// <param name="ventaRepository">Repositorio de ventas.</param>
    /// <param name="productoRepository">Repositorio de productos.</param>
    /// <param name="clienteRepository">Repositorio de clientes.</param>
    public VentaService(
        IVentaRepository ventaRepository,
        IProductoRepository productoRepository,
        IClienteRepository clienteRepository)
    {
        _ventaRepository = ventaRepository;
        _productoRepository = productoRepository;
        _clienteRepository = clienteRepository;
    }

    /// <summary>
    /// Obtiene todas las ventas registradas en el sistema.
    /// Convierte las entidades de dominio en objetos DTO para su exposición.
    /// </summary>
    /// <returns>Resultado con la colección de ventas encontradas.</returns>
    public async Task<Result<IEnumerable<VentaDto>>> ObtenerTodasAsync()
    {
        // Obtiene todas las ventas desde el repositorio.
        var ventas = await _ventaRepository.ObtenerTodasAsync();

        // Convierte cada venta a un DTO.
        var dtos = ventas.Select(v => new VentaDto
        {
            Id = v.Id,
            Fecha = v.Fecha,
            IdCliente = v.IdCliente,
            NombreCliente = v.Cliente?.Nombre ?? string.Empty,
            Total = v.Total,
            Detalles = v.Detalles.Select(d => new DetalleVentaDto
            {
                IdProducto = d.IdProducto,
                NombreProducto = d.Producto?.Nombre ?? string.Empty,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
        });

        return Result<IEnumerable<VentaDto>>.Ok(dtos);
    }

    /// <summary>
    /// Obtiene una venta específica a partir de su identificador.
    /// </summary>
    /// <param name="id">Identificador único de la venta.</param>
    /// <returns>Resultado con la información de la venta encontrada.</returns>
    public async Task<Result<VentaDto>> ObtenerPorIdAsync(int id)
    {
        // Busca la venta por su identificador.
        var venta = await _ventaRepository.ObtenerPorIdAsync(id);

        // Valida que la venta exista.
        if (venta is null)
            return Result<VentaDto>.Fail($"Venta con id {id} no encontrada");

        // Convierte la entidad a DTO.
        return Result<VentaDto>.Ok(new VentaDto
        {
            Id = venta.Id,
            Fecha = venta.Fecha,
            IdCliente = venta.IdCliente,
            NombreCliente = venta.Cliente?.Nombre ?? string.Empty,
            Total = venta.Total,
            Detalles = venta.Detalles.Select(d => new DetalleVentaDto
            {
                IdProducto = d.IdProducto,
                NombreProducto = d.Producto?.Nombre ?? string.Empty,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
        });
    }

    /// <summary>
    /// Registra una nueva venta en el sistema.
    /// Valida la existencia del cliente y productos,
    /// verifica disponibilidad de inventario,
    /// calcula el total de la venta y actualiza el stock.
    /// </summary>
    /// <param name="dto">Datos necesarios para crear una nueva venta.</param>
    /// <returns>Resultado que contiene la información de la venta registrada.</returns>
    public async Task<Result<VentaDto>> CrearAsync(CrearVentaDto dto)
    {
        // Validar cliente.
        var cliente = await _clienteRepository.ObtenerPorIdAsync(dto.IdCliente);

        if (cliente is null)
            return Result<VentaDto>.Fail($"Cliente con id {dto.IdCliente} no encontrado");

        // Verificar que la venta contenga al menos un producto.
        if (dto.Detalles.Count == 0)
            return Result<VentaDto>.Fail("La venta debe tener al menos un producto");

        // Lista donde se almacenarán los detalles de la venta.
        var detalles = new List<DetalleVenta>();

        // Diccionario para guardar el nombre de cada producto por su id,
        // y así reutilizarlo al construir la respuesta sin consultar la BD de nuevo.
        var nombresProductos = new Dictionary<int, string>();

        // Acumulador para calcular el total de la venta.
        decimal total = 0;

        // Procesa cada producto incluido en la venta.
        foreach (var item in dto.Detalles)
        {
            // Busca el producto solicitado.
            var producto = await _productoRepository.ObtenerPorIdAsync(item.IdProducto);

            // Verifica que el producto exista.
            if (producto is null)
                return Result<VentaDto>.Fail($"Producto con id {item.IdProducto} no encontrado");

            // Valida que exista suficiente inventario.
            if (producto.Stock < item.Cantidad)
                return Result<VentaDto>.Fail(
                    $"Stock insuficiente para '{producto.Nombre}'. Disponible: {producto.Stock}");

            // Agrega el detalle de venta.
            detalles.Add(new DetalleVenta
            {
                IdProducto = producto.Id,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio
            });

            // Guarda el nombre del producto para la respuesta.
            nombresProductos[producto.Id] = producto.Nombre;

            // Suma el subtotal al total de la venta.
            total += producto.Precio * item.Cantidad;

            // Descuenta las unidades vendidas del inventario.
            producto.Stock -= item.Cantidad;

            // Actualiza el producto con el nuevo stock.
            await _productoRepository.ActualizarAsync(producto);
        }

        // Crea la entidad de venta.
        var venta = new Venta
        {
            Fecha = DateTime.UtcNow,
            IdCliente = dto.IdCliente,
            Total = total,
            Detalles = detalles
        };

        // Guarda la venta en la base de datos.
        var creada = await _ventaRepository.CrearAsync(venta);

        // Retorna la información de la venta creada,
        // incluyendo el nombre del producto desde el diccionario en memoria.
        return Result<VentaDto>.Ok(new VentaDto
        {
            Id = creada.Id,
            Fecha = creada.Fecha,
            IdCliente = creada.IdCliente,
            NombreCliente = cliente.Nombre,
            Total = creada.Total,
            Detalles = creada.Detalles.Select(d => new DetalleVentaDto
            {
                IdProducto = d.IdProducto,
                NombreProducto = nombresProductos.TryGetValue(d.IdProducto, out var nombre)
                    ? nombre
                    : string.Empty,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
        }, "Venta registrada exitosamente");
    }
}