namespace PruebaTecnica.Application.DTOs.Productos;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para recibir
/// la información necesaria para registrar un nuevo producto
/// dentro del sistema.
/// </summary>
public class CrearProductoDto
{
    /// <summary>
    /// Nombre del producto que será agregado al catálogo.
    /// Debe ser descriptivo para facilitar su identificación.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del producto.
    /// Puede incluir características, especificaciones técnicas
    /// y otra información relevante para los usuarios.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Precio de venta del producto.
    /// Representa el valor monetario por unidad que será utilizado
    /// en las operaciones comerciales.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Cantidad inicial disponible del producto en inventario.
    /// Este valor permite controlar la disponibilidad del producto.
    /// </summary>
    public int Stock { get; set; }
}