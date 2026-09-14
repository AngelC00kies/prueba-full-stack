using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Entities;

/// <summary>
/// Representa un usuario del sistema.
/// Contiene la información necesaria para la autenticación,
/// autorización y administración de accesos dentro de la aplicación.
/// </summary>
public class Usuario
{
    /// <summary>
    /// Identificador único del usuario.
    /// Generalmente corresponde a la clave primaria en la base de datos.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de usuario utilizado para iniciar sesión en el sistema.
    /// Debe ser único para evitar conflictos de autenticación.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña almacenada en formato cifrado o hash.
    /// Nunca debe contener la contraseña en texto plano por razones de seguridad.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Rol asignado al usuario dentro de la aplicación.
    /// Determina los permisos y acciones que puede realizar.
    /// Por defecto se asigna el rol "user".
    /// </summary>
    public string Rol { get; set; } = "user";

    /// <summary>
    /// Indica si la cuenta del usuario se encuentra activa.
    /// Si el valor es false, el acceso al sistema puede ser restringido.
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Fecha y hora de creación del usuario en formato UTC.
    /// Se establece automáticamente al momento de crear la instancia.
    /// </summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}