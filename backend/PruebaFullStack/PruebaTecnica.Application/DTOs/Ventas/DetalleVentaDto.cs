namespace PruebaTecnica.Application.DTOs.Ventas;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para exponer
/// la información detallada de un producto incluido en una venta.
/// Contiene los datos necesarios para mostrar el producto vendido,
/// la cantidad, el precio aplicado y el subtotal calculado.
/// </summary>
public class DetalleVentaDto
{
    /// <summary>
    /// Identificador único del producto vendido.
    /// Permite relacionar el detalle con el producto correspondiente.
    /// </summary>
    public int IdProducto { get; set; }

    /// <summary>
    /// Nombre del producto vendido.
    /// Se incluye para facilitar la visualización de la información
    /// sin necesidad de realizar consultas adicionales.
    /// </summary>
    public string NombreProducto { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad de unidades vendidas del producto.
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del producto al momento de la venta.
    /// Este valor se conserva para mantener el histórico de la transacción.
    /// </summary>
    public decimal PrecioUnitario { get; set; }

    /// <summary>
    /// Subtotal calculado automáticamente para el producto vendido.
    /// Corresponde a la multiplicación de la cantidad por el precio unitario.
    /// </summary>
    public decimal Subtotal => Cantidad * PrecioUnitario;
}