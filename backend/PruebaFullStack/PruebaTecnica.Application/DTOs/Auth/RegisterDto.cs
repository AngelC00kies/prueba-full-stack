namespace PruebaTecnica.Application.DTOs.Auth;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para recibir
/// la información necesaria para registrar un nuevo usuario
/// dentro del sistema.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// Nombre de usuario que se asignará a la nueva cuenta.
    /// Debe ser único para garantizar una correcta identificación
    /// y autenticación dentro de la aplicación.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña proporcionada por el usuario durante el registro.
    /// Este valor debe ser procesado y almacenado de forma segura
    /// mediante un algoritmo de hash antes de persistirse en la base de datos.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Rol asignado al usuario al momento de su creación.
    /// Determina los permisos y funcionalidades a las que tendrá acceso.
    /// Por defecto, se asigna el rol "user".
    /// </summary>
    public string Rol { get; set; } = "user";
}