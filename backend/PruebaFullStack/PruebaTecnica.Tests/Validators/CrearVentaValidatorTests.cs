using FluentAssertions;
using PruebaTecnica.Application.DTOs.Ventas;
using PruebaTecnica.Application.Validators;

namespace PruebaTecnica.Tests.Validators;

public class CrearVentaValidatorTests
{
    private readonly CrearVentaValidator _validator = new();

    [Fact]
    public void Validator_CuandoVentaValida_NoTieneErrores()
    {
        // Arrange
        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new()
            {
                new() { IdProducto = 1, Cantidad = 2 }
            }
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validator_CuandoIdClienteCero_RetornaError()
    {
        // Arrange
        var dto = new CrearVentaDto
        {
            IdCliente = 0,
            Detalles = new() { new() { IdProducto = 1, Cantidad = 1 } }
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.IdCliente));
    }

    [Fact]
    public void Validator_CuandoSinDetalles_RetornaError()
    {
        // Arrange
        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new()
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.Detalles));
    }

    [Fact]
    public void Validator_CuandoCantidadCero_RetornaError()
    {
        // Arrange
        var dto = new CrearVentaDto
        {
            IdCliente = 1,
            Detalles = new() { new() { IdProducto = 1, Cantidad = 0 } }
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}