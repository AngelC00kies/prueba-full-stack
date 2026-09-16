using FluentAssertions;
using Moq;
using PruebaTecnica.Application.DTOs.Auth;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Tests.Builders;

namespace PruebaTecnica.Tests.Services;

/// <summary>
/// Pruebas unitarias para <see cref="AuthService"/>.
/// Verifica el login y registro de usuarios, así como la validación de credenciales.
/// </summary>
public class AuthServiceTests
{
    private readonly Mock<IUsuarioRepository> _repoMock;
    private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _repoMock = new Mock<IUsuarioRepository>();
        _tokenGeneratorMock = new Mock<ITokenGenerator>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _sut = new AuthService(
            _repoMock.Object,
            _tokenGeneratorMock.Object,
            _passwordHasherMock.Object);
    }

    // ==================== LoginAsync ====================

    [Fact]
    public async Task LoginAsync_ConCredencialesValidas_RetornaToken()
    {
        // Arrange
        var dto = new LoginDto { Username = "admin", Password = "admin123" };
        var usuario = new UsuarioBuilder()
            .ConId(1)
            .ConUsername("admin")
            .ConPasswordHash("hashed")
            .ConRol("admin")
            .Build();

        _repoMock.Setup(r => r.ObtenerPorUsernameAsync("admin")).ReturnsAsync(usuario);
        _passwordHasherMock.Setup(h => h.Verify("admin123", "hashed")).Returns(true);
        _tokenGeneratorMock.Setup(t => t.GenerateToken(usuario)).Returns("fake-jwt-token");

        // Act
        var result = await _sut.LoginAsync(dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Token.Should().Be("fake-jwt-token");
        result.Data.Username.Should().Be("admin");
        result.Data.Rol.Should().Be("admin");
    }

    [Fact]
    public async Task LoginAsync_CuandoUsuarioNoExiste_RetornaFail()
    {
        // Arrange
        var dto = new LoginDto { Username = "noexiste", Password = "pass" };
        _repoMock.Setup(r => r.ObtenerPorUsernameAsync("noexiste")).ReturnsAsync((Usuario?)null);

        // Act
        var result = await _sut.LoginAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("Credenciales inválidas");
        _tokenGeneratorMock.Verify(t => t.GenerateToken(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_CuandoPasswordIncorrecta_RetornaFail()
    {
        // Arrange
        var dto = new LoginDto { Username = "admin", Password = "wrong" };
        var usuario = new UsuarioBuilder()
            .ConUsername("admin")
            .ConPasswordHash("hashed")
            .Build();

        _repoMock.Setup(r => r.ObtenerPorUsernameAsync("admin")).ReturnsAsync(usuario);
        _passwordHasherMock.Setup(h => h.Verify("wrong", "hashed")).Returns(false);

        // Act
        var result = await _sut.LoginAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("Credenciales inválidas");
    }

    [Fact]
    public async Task LoginAsync_CuandoUsuarioInactivo_RetornaFail()
    {
        // Arrange
        var dto = new LoginDto { Username = "admin", Password = "pass" };
        var usuario = new UsuarioBuilder()
            .ConUsername("admin")
            .ConActivo(false)
            .Build();

        _repoMock.Setup(r => r.ObtenerPorUsernameAsync("admin")).ReturnsAsync(usuario);

        // Act
        var result = await _sut.LoginAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("Credenciales inválidas");
    }

    // ==================== RegisterAsync ====================

    [Fact]
    public async Task RegisterAsync_ConDatosValidos_RetornaUsuarioCreado()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "nuevo",
            Password = "pass123",
            Rol = "user"
        };

        _repoMock.Setup(r => r.ExisteUsernameAsync("nuevo")).ReturnsAsync(false);
        _passwordHasherMock.Setup(h => h.Hash("pass123")).Returns("hashed_password");

        _repoMock.Setup(r => r.CrearAsync(It.IsAny<Usuario>()))
                 .ReturnsAsync((Usuario u) =>
                 {
                     u.Id = 5;
                     return u;
                 });

        _tokenGeneratorMock.Setup(t => t.GenerateToken(It.IsAny<Usuario>()))
                           .Returns("new-token");

        // Act
        var result = await _sut.RegisterAsync(dto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Token.Should().Be("new-token");
        result.Data.Username.Should().Be("nuevo");
        _passwordHasherMock.Verify(h => h.Hash("pass123"), Times.Once);
        _repoMock.Verify(r => r.CrearAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_CuandoUsernameYaExiste_RetornaFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "admin",
            Password = "pass123",
            Rol = "user"
        };

        _repoMock.Setup(r => r.ExisteUsernameAsync("admin")).ReturnsAsync(true);

        // Act
        var result = await _sut.RegisterAsync(dto);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("ya está en uso");
        _repoMock.Verify(r => r.CrearAsync(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_HasheaLaPasswordAntesDeGuardar()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "user1",
            Password = "plaintext",
            Rol = "user"
        };

        _repoMock.Setup(r => r.ExisteUsernameAsync("user1")).ReturnsAsync(false);
        _passwordHasherMock.Setup(h => h.Hash("plaintext")).Returns("hashed");
        _repoMock.Setup(r => r.CrearAsync(It.IsAny<Usuario>()))
                 .ReturnsAsync((Usuario u) => { u.Id = 1; return u; });
        _tokenGeneratorMock.Setup(t => t.GenerateToken(It.IsAny<Usuario>())).Returns("t");

        // Act
        await _sut.RegisterAsync(dto);

        // Assert
        _repoMock.Verify(r => r.CrearAsync(
            It.Is<Usuario>(u => u.PasswordHash == "hashed")),
            Times.Once);
    }
}