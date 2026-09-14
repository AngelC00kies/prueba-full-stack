namespace PruebaTecnica.Application.DTOs.Auth;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para devolver
/// la información de autenticación de un usuario después de un
/// inicio de sesión exitoso.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// Token JWT generado para el usuario autenticado.
    /// Este token contiene la información necesaria para la
    /// autorización y acceso a los recursos protegidos del sistema.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de usuario autenticado.
    /// Permite identificar la cuenta asociada al token emitido.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Rol asignado al usuario.
    /// Determina los permisos y acciones que puede realizar
    /// dentro de la aplicación.
    /// </summary>
    public string Rol { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de expiración del token de autenticación.
    /// Una vez alcanzada esta fecha, el usuario deberá autenticarse
    /// nuevamente para obtener un nuevo token válido.
    /// </summary>
    public DateTime Expira { get; set; }
}