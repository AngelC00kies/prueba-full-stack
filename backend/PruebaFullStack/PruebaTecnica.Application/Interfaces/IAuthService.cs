using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Auth;

namespace PruebaTecnica.Application.Interfaces;

/// <summary>
/// Define los servicios de autenticación y registro de usuarios.
/// Esta interfaz establece el contrato para las operaciones relacionadas
/// con la seguridad, autenticación y gestión de acceso al sistema.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Autentica un usuario utilizando sus credenciales de acceso.
    /// Si las credenciales son válidas, se genera y devuelve un token
    /// de autenticación junto con la información del usuario.
    /// </summary>
    /// <param name="dto">
    /// Credenciales proporcionadas por el usuario para iniciar sesión.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información de autenticación,
    /// incluyendo el token de acceso y los datos básicos del usuario.
    /// </returns>
    Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// Si el proceso se completa correctamente, se devuelve la
    /// información de autenticación correspondiente al usuario creado.
    /// </summary>
    /// <param name="dto">
    /// Información requerida para crear una nueva cuenta de usuario.
    /// </param>
    /// <returns>
    /// Un resultado que contiene la información de autenticación
    /// del usuario registrado.
    /// </returns>
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);
}