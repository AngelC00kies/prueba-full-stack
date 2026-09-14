namespace PruebaTecnica.Domain.Entities;

/// <summary>
/// Representa una venta realizada a un cliente.
/// Contiene la información general de la transacción,
/// incluyendo la fecha, el cliente asociado, el monto total
/// y el detalle de los productos vendidos.
/// </summary>
public class Venta
{
    /// <summary>
    /// Identificador único de la venta.
    /// Generalmente corresponde a la clave primaria en la base de datos.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Fecha y hora en que se registró la venta.
    /// Se inicializa automáticamente con la fecha y hora actual en formato UTC.
    /// </summary>
    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Identificador del cliente que realizó la compra.
    /// Actúa como clave foránea hacia la entidad Cliente.
    /// </summary>
    public int IdCliente { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia el cliente asociado a la venta.
    /// Permite acceder a la información completa del cliente.
    /// </summary>
    public Cliente? Cliente { get; set; }

    /// <summary>
    /// Monto total de la venta.
    /// Corresponde a la suma de los subtotales de todos los productos
    /// incluidos en el detalle de la transacción.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Colección de detalles asociados a la venta.
    /// Cada elemento representa un producto vendido,
    /// indicando cantidad y precio unitario.
    /// </summary>
    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}