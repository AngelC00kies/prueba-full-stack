using FluentAssertions;
using Moq;
using PruebaTecnica.Application.DTOs.Productos;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Tests.Builders;

namespace PruebaTecnica.Tests.Services;

/// <summary>
/// Pruebas unitarias para <see cref="ProductoService"/>.
/// Verifica el correcto funcionamiento de las operaciones CRUD de productos.
/// </summary>
public class ProductoServiceTests
{
    private readonly Mock<IProductoRepository> _repoMock;
    private readonly ProductoService _sut;   // System Under Test

    public ProductoServiceTests()
    {
        _repoMock = new Mock<IProductoRepository>();
        _sut = new ProductoService(_repoMock.Object);
    }

    // ==================== ObtenerTodosAsync ====================

    [Fact]
    public async Task ObtenerTodosAsync_CuandoHayProductos_RetornaListaMapeada()
    {
        // Arrange
        var productos = new List<Producto>
        {
            new ProductoBuilder().ConId(1).ConNombre("Laptop").Build(),
            new ProductoBuilder().ConId(2).ConNombre("Mouse").Build()
        };
        _repoMock.Setup(r => r.ObtenerTodosAsync()).ReturnsAsync(productos);

        // Act
        var result = await _sut.ObtenerTodosAsync();

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(2);
        result.Data!.First().Nombre.Should().Be("Laptop");
        _repoMock.Verify(r => r.ObtenerTodosAsync(), Times.Once);
    }

    [Fact]
    public async Task ObtenerTodosAsync_CuandoNoHayProductos_RetornaListaVacia()
    {
        // Arrange
        _repoMock.Setup(r => r.ObtenerTodosAsync())
                 .ReturnsAsync(new List<Producto>());

        // Act
        var result = await _sut.ObtenerTodosAsync();

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().BeEmpty();
    }

    // ==================== ObtenerPorIdAsync ====================

    [Fact]
    public async Task ObtenerPorIdAsync_CuandoExiste_RetornaProducto()
    {
        // Arrange
        var producto = new ProductoBuilder().ConId(5).ConNombre("Teclado").Build();
        _repoMock.Setup(r => r.ObtenerPorIdAsync(5)).ReturnsAsync(producto);

        // Act
        var result = await _sut.ObtenerPorIdAsync(5);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Id.Should().Be(5);
        result.Data.Nombre.Should().Be("Teclado");
    }

    [Fact]
    public async Task ObtenerPorIdAsync_CuandoNoExiste_RetornaFail()
    {
        // Arrange
        _repoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Producto?)null);

        // Act
        var result = await _sut.ObtenerPorIdAsync(99);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("99");
        result.Data.Should().BeNull();
    }

    // ==================== CrearAsync ====================

    [Fact]
    public async Task CrearAsync_ConDatosValidos_RetornaProductoCreado()
    {
        // Arrange
        var dto = new CrearProductoDto
        {
            Nombre = "Monitor",
            Descripcion = "Monitor 24 pulgadas",
            Precio = 300m,
            Stock = 5
        };

        _repoMock.Setup(r => r.CrearAsync(It.IsAny<Producto>()))
                 .ReturnsAsync((Producto p) =>
                 {
                     p.Id = 10;
                     return p;
                 });

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Id.Should().Be(10);
        result.Data.Nombre.Should().Be("Monitor");
        result.Data.Precio.Should().Be(300m);
        _repoMock.Verify(r => r.CrearAsync(It.IsAny<Producto>()), Times.Once);
    }

    // ==================== ActualizarAsync ====================

    [Fact]
    public async Task ActualizarAsync_CuandoExiste_RetornaProductoActualizado()
    {
        // Arrange
        var productoExistente = new ProductoBuilder().ConId(1).ConNombre("Viejo").Build();
        var dto = new ActualizarProductoDto
        {
            Nombre = "Nuevo",
            Descripcion = "Actualizado",
            Precio = 200m,
            Stock = 15
        };

        _repoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(productoExistente);
        _repoMock.Setup(r => r.ActualizarAsync(It.IsAny<Producto>()))
                 .ReturnsAsync((Producto p) => p);

        // Act
        var result = await _sut.ActualizarAsync(1, dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Nombre.Should().Be("Nuevo");
        result.Data.Precio.Should().Be(200m);
    }

    [Fact]
    public async Task ActualizarAsync_CuandoNoExiste_RetornaFail()
    {
        // Arrange
        var dto = new ActualizarProductoDto { Nombre = "X", Precio = 10, Stock = 1 };
        _repoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Producto?)null);

        // Act
        var result = await _sut.ActualizarAsync(99, dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("99");
    }

    // ==================== EliminarAsync ====================

    [Fact]
    public async Task EliminarAsync_CuandoExiste_RetornaExito()
    {
        // Arrange
        _repoMock.Setup(r => r.ExisteAsync(1)).ReturnsAsync(true);
        _repoMock.Setup(r => r.EliminarAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _sut.EliminarAsync(1);

        // Assert
        result.Success.Should().BeTrue();
        _repoMock.Verify(r => r.EliminarAsync(1), Times.Once);
    }

    [Fact]
    public async Task EliminarAsync_CuandoNoExiste_RetornaFail()
    {
        // Arrange
        _repoMock.Setup(r => r.ExisteAsync(99)).ReturnsAsync(false);

        // Act
        var result = await _sut.EliminarAsync(99);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("99");
        _repoMock.Verify(r => r.EliminarAsync(It.IsAny<int>()), Times.Never);
    }
}