namespace PruebaTecnica.Application.DTOs.Clientes;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para exponer
/// la información de un cliente hacia capas externas de la aplicación,
/// como la API o la interfaz de usuario.
/// </summary>
public class ClienteDto
{
    /// <summary>
    /// Identificador único del cliente.
    /// Permite distinguir al cliente dentro del sistema.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del cliente.
    /// Representa la información principal de identificación.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del cliente.
    /// Utilizada como medio de comunicación y contacto.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono registrado para el cliente.
    /// Facilita la comunicación y el seguimiento comercial.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;
}