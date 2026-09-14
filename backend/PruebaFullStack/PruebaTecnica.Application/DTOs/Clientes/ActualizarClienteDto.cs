namespace PruebaTecnica.Application.DTOs.Clientes;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para recibir
/// la información necesaria para actualizar los datos de un cliente
/// existente dentro del sistema.
/// </summary>
public class ActualizarClienteDto
{
    /// <summary>
    /// Nombre completo del cliente.
    /// Este valor será utilizado para actualizar la información
    /// de identificación del cliente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del cliente.
    /// Se utiliza como medio de contacto y puede requerir validación
    /// para garantizar un formato correcto.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono del cliente.
    /// Permite mantener actualizada la información de contacto.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;
}