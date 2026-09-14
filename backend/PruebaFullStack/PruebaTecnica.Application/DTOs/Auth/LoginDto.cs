namespace PruebaTecnica.Application.DTOs.Auth;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para recibir
/// las credenciales de autenticación enviadas por un usuario
/// durante el proceso de inicio de sesión.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Nombre de usuario utilizado para autenticarse en el sistema.
    /// Debe coincidir con un usuario registrado y activo.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña proporcionada por el usuario durante el proceso
    /// de autenticación. Este valor será validado contra la
    /// contraseña almacenada de forma segura en el sistema.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}