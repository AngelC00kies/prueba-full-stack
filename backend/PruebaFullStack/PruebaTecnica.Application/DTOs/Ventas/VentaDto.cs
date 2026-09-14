namespace PruebaTecnica.Application.DTOs.Ventas;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para exponer
/// la información completa de una venta, incluyendo los datos
/// generales de la transacción, el cliente asociado y el detalle
/// de los productos vendidos.
/// </summary>
public class VentaDto
{
    /// <summary>
    /// Identificador único de la venta.
    /// Permite distinguir la transacción dentro del sistema.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Fecha y hora en que se realizó la venta.
    /// Se utiliza para el control y seguimiento histórico
    /// de las transacciones.
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Identificador del cliente asociado a la venta.
    /// Permite relacionar la transacción con el cliente correspondiente.
    /// </summary>
    public int IdCliente { get; set; }

    /// <summary>
    /// Nombre del cliente que realizó la compra.
    /// Se incluye para facilitar la visualización de la información
    /// sin necesidad de realizar consultas adicionales.
    /// </summary>
    public string NombreCliente { get; set; } = string.Empty;

    /// <summary>
    /// Monto total de la venta.
    /// Corresponde a la suma de todos los subtotales
    /// de los productos incluidos en la transacción.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Colección de detalles asociados a la venta.
    /// Cada elemento representa un producto vendido,
    /// incluyendo cantidad, precio unitario y subtotal.
    /// </summary>
    public List<DetalleVentaDto> Detalles { get; set; } = new();
}