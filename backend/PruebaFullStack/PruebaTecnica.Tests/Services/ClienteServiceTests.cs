using FluentAssertions;
using Moq;
using PruebaTecnica.Application.DTOs.Clientes;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Tests.Builders;

namespace PruebaTecnica.Tests.Services;

/// <summary>
/// Pruebas unitarias para <see cref="ClienteService"/>.
/// Verifica el correcto funcionamiento de las operaciones CRUD de clientes.
/// </summary>
public class ClienteServiceTests
{
    private readonly Mock<IClienteRepository> _repoMock;
    private readonly ClienteService _sut;   // System Under Test

    public ClienteServiceTests()
    {
        _repoMock = new Mock<IClienteRepository>();
        _sut = new ClienteService(_repoMock.Object);
    }

    // ==================== ObtenerTodosAsync ====================

    [Fact]
    public async Task ObtenerTodosAsync_CuandoHayClientes_RetornaListaMapeada()
    {
        // Arrange
        var clientes = new List<Cliente>
        {
            new ClienteBuilder().ConId(1).ConNombre("Juan Perez").Build(),
            new ClienteBuilder().ConId(2).ConNombre("Maria Lopez").Build()
        };
        _repoMock.Setup(r => r.ObtenerTodosAsync()).ReturnsAsync(clientes);

        // Act
        var result = await _sut.ObtenerTodosAsync();

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(2);
        result.Data!.First().Nombre.Should().Be("Juan Perez");
        _repoMock.Verify(r => r.ObtenerTodosAsync(), Times.Once);
    }

    [Fact]
    public async Task ObtenerTodosAsync_CuandoNoHayClientes_RetornaListaVacia()
    {
        // Arrange
        _repoMock.Setup(r => r.ObtenerTodosAsync())
                 .ReturnsAsync(new List<Cliente>());

        // Act
        var result = await _sut.ObtenerTodosAsync();

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().BeEmpty();
    }

    // ==================== ObtenerPorIdAsync ====================

    [Fact]
    public async Task ObtenerPorIdAsync_CuandoExiste_RetornaCliente()
    {
        // Arrange
        var cliente = new ClienteBuilder().ConId(5).ConNombre("Carlos Ruiz").Build();
        _repoMock.Setup(r => r.ObtenerPorIdAsync(5)).ReturnsAsync(cliente);

        // Act
        var result = await _sut.ObtenerPorIdAsync(5);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Id.Should().Be(5);
        result.Data.Nombre.Should().Be("Carlos Ruiz");
    }

    [Fact]
    public async Task ObtenerPorIdAsync_CuandoNoExiste_RetornaFail()
    {
        // Arrange
        _repoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Cliente?)null);

        // Act
        var result = await _sut.ObtenerPorIdAsync(99);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("99");
        result.Data.Should().BeNull();
    }

    // ==================== CrearAsync ====================

    [Fact]
    public async Task CrearAsync_ConDatosValidos_RetornaClienteCreado()
    {
        // Arrange
        var dto = new CrearClienteDto
        {
            Nombre = "Ana Torres",
            Email = "ana@test.com",
            Telefono = "555-9999"
        };

        _repoMock.Setup(r => r.CrearAsync(It.IsAny<Cliente>()))
                 .ReturnsAsync((Cliente c) =>
                 {
                     c.Id = 10;
                     return c;
                 });

        // Act
        var result = await _sut.CrearAsync(dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Id.Should().Be(10);
        result.Data.Nombre.Should().Be("Ana Torres");
        result.Data.Email.Should().Be("ana@test.com");
        _repoMock.Verify(r => r.CrearAsync(It.IsAny<Cliente>()), Times.Once);
    }

    // ==================== ActualizarAsync ====================

    [Fact]
    public async Task ActualizarAsync_CuandoExiste_RetornaClienteActualizado()
    {
        // Arrange
        var existente = new ClienteBuilder().ConId(1).ConNombre("Viejo").Build();
        var dto = new ActualizarClienteDto
        {
            Nombre = "Nuevo Nombre",
            Email = "nuevo@test.com",
            Telefono = "555-0000"
        };

        _repoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(existente);
        _repoMock.Setup(r => r.ActualizarAsync(It.IsAny<Cliente>()))
                 .ReturnsAsync((Cliente c) => c);

        // Act
        var result = await _sut.ActualizarAsync(1, dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Nombre.Should().Be("Nuevo Nombre");
        result.Data.Email.Should().Be("nuevo@test.com");
    }

    [Fact]
    public async Task ActualizarAsync_CuandoNoExiste_RetornaFail()
    {
        // Arrange
        var dto = new ActualizarClienteDto
        {
            Nombre = "X",
            Email = "x@test.com",
            Telefono = "555"
        };
        _repoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Cliente?)null);

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