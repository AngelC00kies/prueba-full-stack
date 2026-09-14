namespace PruebaTecnica.Application.DTOs.Clientes;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para recibir
/// la información necesaria para registrar un nuevo cliente
/// dentro del sistema.
/// </summary>
public class CrearClienteDto
{
    /// <summary>
    /// Nombre completo del cliente que será registrado.
    /// Representa el dato principal de identificación del cliente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del cliente.
    /// Se utiliza como medio de contacto y comunicación.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono de contacto del cliente.
    /// Permite establecer comunicación directa cuando sea necesario.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;
}