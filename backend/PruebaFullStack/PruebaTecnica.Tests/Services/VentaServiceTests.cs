using FluentAssertions;
using Moq;
using PruebaTecnica.Application.DTOs.Ventas;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Tests.Builders;

namespace PruebaTecnica.Tests.Services;

/// <summary>
/// Pruebas unitarias para <see cref="VentaService"/>.
/// Verifica el registro de ventas, validación de stock y cálculo de totales.
/// </summary>
public class VentaServiceTests
{
    private readonly Mock<IVentaRepository> _ventaRepoMock;
    private readonly Mock<IProductoRepository> _productoRepoMock;
    private readonly Mock<IClienteRepository> _clienteRepoMock;
    private readonly VentaService _sut;

    public VentaServiceTests()
    {
        _ventaRepoMock = new Mock<IVentaRepository>();
        _productoRepoMock = new Mock<IProductoRepository>();
        _clienteRepoMock = new Mock<IClienteRepository>();

        _sut = new VentaService(
            _ventaRepoMock.Object,
            _productoRepoMock.Object,
            _clienteRepoMock.Object);
    }

    // ==================== CrearAsync — Casos de error ====================

    [Fact]
    public async Task CrearAsync_CuandoClienteNoExiste_RetornaFail()
    {
        // Arrange
        var dto = new CrearVentaDto
        {
            IdCliente = 99,
            Detalles = new List<CrearDetalleVentaDto>
            {
                new() { IdProducto = 1, Cantidad = 1 }
            }
        };
        _clienteRepoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Cliente?)null);

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("99");
    }

    [Fact]
    public async Task CrearAsync_CuandoNoHayDetalles_RetornaFail()
    {
        // Arrange
        var cliente = new ClienteBuilder().ConId(1).Build();
        var dto = new CrearVentaDto { IdCliente = 1, Detalles = new() };

        _clienteRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(cliente);

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("al menos un producto");
    }

    [Fact]
    public async Task CrearAsync_CuandoProductoNoExiste_RetornaFail()
    {
        // Arrange
        var cliente = new ClienteBuilder().ConId(1).Build();
        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new() { new() { IdProducto = 99, Cantidad = 1 } }
        };

        _clienteRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(cliente);
        _productoRepoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Producto?)null);

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("99");
    }

    [Fact]
    public async Task CrearAsync_CuandoStockInsuficiente_RetornaFail()
    {
        // Arrange
        var cliente = new ClienteBuilder().ConId(1).Build();
        var producto = new ProductoBuilder().ConId(1).ConNombre("Laptop").ConStock(2).Build();
        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new() { new() { IdProducto = 1, Cantidad = 10 } }
        };

        _clienteRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(cliente);
        _productoRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(producto);

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("Stock insuficiente");
        result.Message.Should().Contain("Laptop");
    }

    // ==================== CrearAsync — Caso exitoso ====================

    [Fact]
    public async Task CrearAsync_ConDatosValidos_RetornaVentaCreada()
    {
        // Arrange
        var cliente = new ClienteBuilder().ConId(1).ConNombre("Juan").Build();
        var producto = new ProductoBuilder()
            .ConId(1)
            .ConNombre("Laptop")
            .ConPrecio(1000m)
            .ConStock(10)
            .Build();

        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new() { new() { IdProducto = 1, Cantidad = 2 } }
        };

        _clienteRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(cliente);
        _productoRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(producto);
        _productoRepoMock.Setup(r => r.ActualizarAsync(It.IsAny<Producto>()))
                         .ReturnsAsync((Producto p) => p);

        _ventaRepoMock.Setup(r => r.CrearAsync(It.IsAny<Venta>()))
                      .ReturnsAsync((Venta v) =>
                      {
                          v.Id = 1;
                          return v;
                      });

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Total.Should().Be(2000m);
        result.Data.NombreCliente.Should().Be("Juan");
        result.Data.Detalles.Should().HaveCount(1);
        result.Data.Detalles.First().NombreProducto.Should().Be("Laptop");

        // Verifica que el stock se descontó correctamente
        producto.Stock.Should().Be(8);
        _productoRepoMock.Verify(r => r.ActualizarAsync(producto), Times.Once);
    }

    [Fact]
    public async Task CrearAsync_ConMultiplesProductos_CalculaTotalCorrectamente()
    {
        // Arrange
        var cliente = new ClienteBuilder().ConId(1).Build();
        var producto1 = new ProductoBuilder().ConId(1).ConNombre("Laptop").ConPrecio(1000m).ConStock(10).Build();
        var producto2 = new ProductoBuilder().ConId(2).ConNombre("Mouse").ConPrecio(50m).ConStock(20).Build();

        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new()
            {
                new() { IdProducto = 1, Cantidad = 1 },   // 1000
                new() { IdProducto = 2, Cantidad = 2 }    // 100
            }
        };

        _clienteRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(cliente);
        _productoRepoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(producto1);
        _productoRepoMock.Setup(r => r.ObtenerPorIdAsync(2)).ReturnsAsync(producto2);
        _productoRepoMock.Setup(r => r.ActualizarAsync(It.IsAny<Producto>()))
                         .ReturnsAsync((Producto p) => p);

        _ventaRepoMock.Setup(r => r.CrearAsync(It.IsAny<Venta>()))
                      .ReturnsAsync((Venta v) => { v.Id = 1; return v; });

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Total.Should().Be(1100m);   // 1000 + 100
        result.Data.Detalles.Should().HaveCount(2);
    }
}