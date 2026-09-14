namespace PruebaTecnica.Domain.Entities;

/// <summary>
/// Representa a un cliente registrado en el sistema de gestión de ventas.
/// Cada cliente puede tener múltiples ventas asociadas.
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador único del cliente (clave primaria, autoincremental).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo o razón social del cliente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico de contacto del cliente.
    /// Se utiliza para notificaciones y como dato de identificación.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número telefónico de contacto del cliente.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;
}