using FluentAssertions;
using PruebaTecnica.Application.DTOs.Clientes;
using PruebaTecnica.Application.Validators;

namespace PruebaTecnica.Tests.Validators;

public class CrearClienteValidatorTests
{
    private readonly CrearClienteValidator _validator = new();

    [Fact]
    public void Validator_CuandoClienteValido_NoTieneErrores()
    {
        // Arrange
        var dto = new CrearClienteDto
        {
            Nombre = "Juan Perez",
            Email = "juan@test.com",
            Telefono = "555-1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("", "juan@test.com", "555-1234")]        // Nombre vacío
    [InlineData("Juan", "", "555-1234")]                  // Email vacío
    [InlineData("Juan", "juan@test.com", "")]             // Teléfono vacío
    public void Validator_CuandoCamposVacios_RetornaErrores(
        string nombre, string email, string telefono)
    {
        // Arrange
        var dto = new CrearClienteDto
        {
            Nombre = nombre,
            Email = email,
            Telefono = telefono
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("noesunemail")]
    [InlineData("sin-arroba.com")]
    [InlineData("@sinusuario.com")]
    [InlineData("usuario@")]
    public void Validator_CuandoEmailInvalido_RetornaError(string emailInvalido)
    {
        // Arrange
        var dto = new CrearClienteDto
        {
            Nombre = "Juan",
            Email = emailInvalido,
            Telefono = "555-1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.Email));
    }

    [Fact]
    public void Validator_CuandoNombreMuyLargo_RetornaError()
    {
        // Arrange
        var dto = new CrearClienteDto
        {
            Nombre = new string('A', 150),   // > 100 permitidos
            Email = "juan@test.com",
            Telefono = "555-1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.Nombre));
    }
}