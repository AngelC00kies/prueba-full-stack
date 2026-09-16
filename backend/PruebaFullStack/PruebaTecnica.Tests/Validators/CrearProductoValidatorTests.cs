using FluentAssertions;
using PruebaTecnica.Application.DTOs.Productos;
using PruebaTecnica.Application.Validators;

namespace PruebaTecnica.Tests.Validators;

public class CrearProductoValidatorTests
{
    private readonly CrearProductoValidator _validator = new();

    [Fact]
    public void Validator_CuandoDtoValido_NoTieneErrores()
    {
        // Arrange
        var dto = new CrearProductoDto
        {
            Nombre = "Laptop",
            Descripcion = "Laptop gaming",
            Precio = 1500m,
            Stock = 10
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("", "Desc", 100, 10)]              // Nombre vacío
    [InlineData("Laptop", "", 100, 10)]            // Descripción vacía
    [InlineData("Laptop", "Desc", 0, 10)]          // Precio 0
    [InlineData("Laptop", "Desc", -10, 10)]        // Precio negativo
    [InlineData("Laptop", "Desc", 100, -1)]        // Stock negativo
    public void Validator_CuandoDatosInvalidos_RetornaErrores(
        string nombre, string descripcion, decimal precio, int stock)
    {
        // Arrange
        var dto = new CrearProductoDto
        {
            Nombre = nombre,
            Descripcion = descripcion,
            Precio = precio,
            Stock = stock
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public void Validator_CuandoNombreMuyLargo_RetornaError()
    {
        // Arrange
        var dto = new CrearProductoDto
        {
            Nombre = new string('A', 50),   // 50 caracteres > 30 permitidos
            Descripcion = "Desc",
            Precio = 100m,
            Stock = 10
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.Nombre));
    }
}