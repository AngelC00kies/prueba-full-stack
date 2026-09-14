namespace PruebaTecnica.Domain.Entities;

/// <summary>
/// Representa un producto disponible para la venta dentro del sistema.
/// Contiene la información básica del producto, incluyendo
/// su descripción, precio y cantidad disponible en inventario.
/// </summary>
public class Producto
{
    /// <summary>
    /// Identificador único del producto.
    /// Generalmente corresponde a la clave primaria en la base de datos.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre comercial del producto.
    /// Permite identificar el producto dentro del catálogo.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del producto.
    /// Puede incluir características, especificaciones
    /// o información relevante para el usuario.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Precio de venta actual del producto.
    /// Este valor se utiliza como referencia para generar
    /// los detalles de una venta.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Cantidad disponible del producto en inventario.
    /// Se actualiza conforme se realizan entradas o salidas de stock.
    /// </summary>
    public int Stock { get; set; }
}