using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Auth;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

/// <summary>
/// Servicio encargado de gestionar los procesos de autenticación
/// y registro de usuarios dentro de la aplicación.
/// </summary>
public class AuthService : IAuthService
{
    /// <summary>
    /// Repositorio para el acceso y gestión de usuarios.
    /// </summary>
    private readonly IUsuarioRepository _usuarioRepository;

    /// <summary>
    /// Servicio responsable de la generación de tokens de autenticación.
    /// </summary>
    private readonly ITokenGenerator _tokenGenerator;

    /// <summary>
    /// Servicio encargado de generar y validar hashes de contraseñas.
    /// </summary>
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Constructor que inicializa las dependencias necesarias
    /// para el funcionamiento del servicio de autenticación.
    /// </summary>
    /// <param name="usuarioRepository">Repositorio de usuarios.</param>
    /// <param name="tokenGenerator">Generador de tokens JWT.</param>
    /// <param name="passwordHasher">Servicio para el manejo de contraseñas.</param>
    public AuthService(
        IUsuarioRepository usuarioRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Realiza el proceso de autenticación de un usuario.
    /// Valida la existencia del usuario, el estado de la cuenta
    /// y la contraseña proporcionada antes de generar un token de acceso.
    /// </summary>
    /// <param name="dto">Credenciales utilizadas para iniciar sesión.</param>
    /// <returns>
    /// Resultado que contiene la información de autenticación
    /// si el proceso es exitoso.
    /// </returns>
    public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        // Busca el usuario a partir del nombre de usuario suministrado.
        var usuario = await _usuarioRepository.ObtenerPorUsernameAsync(dto.Username);

        // Verifica que el usuario exista y que su cuenta esté activa.
        if (usuario is null || !usuario.Activo)
            return Result<AuthResponseDto>.Fail("Credenciales inválidas");

        // Valida que la contraseña ingresada coincida con el hash almacenado.
        if (!_passwordHasher.Verify(dto.Password, usuario.PasswordHash))
            return Result<AuthResponseDto>.Fail("Credenciales inválidas");

        // Genera el token de acceso para el usuario autenticado.
        var token = _tokenGenerator.GenerateToken(usuario);

        // Retorna la respuesta de autenticación.
        return Result<AuthResponseDto>.Ok(new AuthResponseDto
        {
            Token = token,
            Username = usuario.Username,
            Rol = usuario.Rol,
            Expira = DateTime.UtcNow.AddHours(2)
        }, "Login exitoso");
    }

    /// <summary>
    /// Registra un nuevo usuario dentro del sistema.
    /// Verifica que el nombre de usuario no exista previamente,
    /// genera el hash de la contraseña y crea la cuenta.
    /// </summary>
    /// <param name="dto">Información necesaria para el registro.</param>
    /// <returns>
    /// Resultado que contiene la información de autenticación
    /// del usuario recién creado.
    /// </returns>
    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto)
    {
        // Verifica que el nombre de usuario no esté registrado.
        if (await _usuarioRepository.ExisteUsernameAsync(dto.Username))
            return Result<AuthResponseDto>.Fail("El username ya está en uso");

        // Crea la entidad Usuario con la información recibida.
        var usuario = new Usuario
        {
            Username = dto.Username,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            Rol = dto.Rol,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        // Guarda el usuario en la base de datos.
        var creado = await _usuarioRepository.CrearAsync(usuario);

        // Genera un token para el nuevo usuario.
        var token = _tokenGenerator.GenerateToken(creado);

        // Retorna la respuesta con la información de autenticación.
        return Result<AuthResponseDto>.Ok(new AuthResponseDto
        {
            Token = token,
            Username = creado.Username,
            Rol = creado.Rol,
            Expira = DateTime.UtcNow.AddHours(2)
        }, "Usuario registrado exitosamente");
    }
}