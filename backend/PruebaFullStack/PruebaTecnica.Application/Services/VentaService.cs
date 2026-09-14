using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Ventas;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IClienteRepository _clienteRepository;

    public VentaService(
        IVentaRepository ventaRepository,
        IProductoRepository productoRepository,
        IClienteRepository clienteRepository)
    {
        _ventaRepository = ventaRepository;
        _productoRepository = productoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<Result<IEnumerable<VentaDto>>> ObtenerTodasAsync()
    {
        var ventas = await _ventaRepository.ObtenerTodasAsync();
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

    public async Task<Result<VentaDto>> ObtenerPorIdAsync(int id)
    {
        var venta = await _ventaRepository.ObtenerPorIdAsync(id);
        if (venta is null)
            return Result<VentaDto>.Fail($"Venta con id {id} no encontrada");

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

    public async Task<Result<VentaDto>> CrearAsync(CrearVentaDto dto)
    {
        // Validar cliente
        var cliente = await _clienteRepository.ObtenerPorIdAsync(dto.Id);
        if (cliente is null)
            return Result<VentaDto>.Fail($"Cliente con id {dto.Id} no encontrado");

        if (dto.Detalles.Count == 0)
            return Result<VentaDto>.Fail("La venta debe tener al menos un producto");

        var detalles = new List<DetalleVenta>();
        decimal total = 0;

        foreach (var item in dto.Detalles)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(item.Id);
            if (producto is null)
                return Result<VentaDto>.Fail($"Producto con id {item.Id} no encontrado");

            if (producto.Stock < item.Cantidad)
                return Result<VentaDto>.Fail($"Stock insuficiente para '{producto.Nombre}'. Disponible: {producto.Stock}");

            detalles.Add(new DetalleVenta
            {
                IdProducto = producto.Id,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio
            });

            total += producto.Precio * item.Cantidad;

            // Descontar stock
            producto.Stock -= item.Cantidad;
            await _productoRepository.ActualizarAsync(producto);
        }

        var venta = new Venta
        {
            Fecha = DateTime.UtcNow,
            IdCliente = dto.Id,
            Total = total,
            Detalles = detalles
        };

        var creada = await _ventaRepository.CrearAsync(venta);

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
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
        }, "Venta registrada exitosamente");
    }
}