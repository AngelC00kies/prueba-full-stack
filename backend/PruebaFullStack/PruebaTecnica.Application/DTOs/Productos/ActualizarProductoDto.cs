namespace PruebaTecnica.Application.DTOs.Productos;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para recibir
/// la información necesaria para actualizar un producto existente
/// dentro del sistema.
/// </summary>
public class ActualizarProductoDto
{
    /// <summary>
    /// Nombre del producto.
    /// Este valor será utilizado para actualizar la identificación
    /// comercial del producto dentro del catálogo.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del producto.
    /// Puede incluir características, especificaciones técnicas
    /// o información complementaria relevante.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Precio de venta actual del producto.
    /// Debe representar un valor monetario válido y mayor o igual a cero.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Cantidad disponible del producto en inventario.
    /// Se utiliza para controlar la disponibilidad y gestión de stock.
    /// </summary>
    public int Stock { get; set; }
}