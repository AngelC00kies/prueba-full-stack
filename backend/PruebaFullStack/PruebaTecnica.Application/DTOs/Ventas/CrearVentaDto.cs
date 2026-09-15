namespace PruebaTecnica.Application.DTOs.Ventas;

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para registrar
/// una nueva venta dentro del sistema.
/// Contiene la información principal de la venta y el listado
/// de productos que formarán parte de la transacción.
/// </summary>
public class CrearVentaDto
{
    /// <summary>
    /// Identificador del cliente que realiza la compra.
    /// Se utiliza para asociar la venta con un cliente existente.
    /// </summary>
    public int IdCliente { get; set; }

    /// <summary>
    /// Colección de productos incluidos en la venta.
    /// Cada elemento representa un detalle con el producto seleccionado
    /// y la cantidad solicitada.
    /// </summary>
    public List<CrearDetalleVentaDto> Detalles { get; set; } = new();
}

/// <summary>
/// Objeto de transferencia de datos (DTO) utilizado para representar
/// el detalle de un producto dentro de una nueva venta.
/// </summary>
public class CrearDetalleVentaDto
{
    /// <summary>
    /// Identificador del producto que se incluirá en la venta.
    /// Debe corresponder a un producto existente en el sistema.
    /// </summary>
    public int IdProducto { get; set; }

    /// <summary>
    /// Cantidad de unidades del producto que serán vendidas.
    /// Debe ser un valor mayor que cero.
    /// </summary>
    public int Cantidad { get; set; }
}