namespace PruebaTecnica.Application.DTOs.Productos;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para exponer
/// la información de un producto hacia capas externas de la aplicación,
/// como la API, aplicaciones cliente o interfaces de usuario.
/// </summary>
public class ProductoDto
{
    /// <summary>
    /// Identificador único del producto.
    /// Permite distinguir el producto dentro del sistema.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre comercial del producto.
    /// Se utiliza para identificarlo dentro del catálogo.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del producto.
    /// Puede contener características, especificaciones técnicas
    /// o información relevante para el usuario.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Precio actual de venta del producto.
    /// Representa el valor monetario por unidad.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Cantidad disponible del producto en inventario.
    /// Permite conocer la disponibilidad del artículo para la venta.
    /// </summary>
    public int Stock { get; set; }
}