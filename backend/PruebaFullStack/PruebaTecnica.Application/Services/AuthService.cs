using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Auth;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.ObtenerPorUsernameAsync(dto.Username);
        if (usuario is null || !usuario.Activo)
            return Result<AuthResponseDto>.Fail("Credenciales inválidas");

        if (!_passwordHasher.Verify(dto.Password, usuario.PasswordHash))
            return Result<AuthResponseDto>.Fail("Credenciales inválidas");

        var token = _tokenGenerator.GenerateToken(usuario);

        return Result<AuthResponseDto>.Ok(new AuthResponseDto
        {
            Token = token,
            Username = usuario.Username,
            Rol = usuario.Rol,
            Expira = DateTime.UtcNow.AddHours(2)
        }, "Login exitoso");
    }

    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto)
    {
        if (await _usuarioRepository.ExisteUsernameAsync(dto.Username))
            return Result<AuthResponseDto>.Fail("El username ya está en uso");

        var usuario = new Usuario
        {
            Username = dto.Username,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            Rol = dto.Rol,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        var creado = await _usuarioRepository.CrearAsync(usuario);
        var token = _tokenGenerator.GenerateToken(creado);

        return Result<AuthResponseDto>.Ok(new AuthResponseDto
        {
            Token = token,
            Username = creado.Username,
            Rol = creado.Rol,
            Expira = DateTime.UtcNow.AddHours(2)
        }, "Usuario registrado exitosamente");
    }
}