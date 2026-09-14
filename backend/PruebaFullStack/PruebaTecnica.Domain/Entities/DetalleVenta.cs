using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Entities;

/// <summary>
/// Representa el detalle de una venta realizada.
/// Cada registro especifica un producto incluido en la venta,
/// junto con su cantidad y precio unitario al momento de la transacción.
/// </summary>
public class DetalleVenta
{
    /// <summary>
    /// Identificador único del detalle de venta.
    /// Generalmente corresponde a la clave primaria de la tabla.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la venta a la que pertenece este detalle.
    /// Funciona como clave foránea hacia la entidad Venta.
    /// </summary>
    public int IdVenta { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia la venta asociada.
    /// Permite acceder a la información completa de la venta
    /// desde el detalle de venta.
    /// </summary>
    public Venta? Venta { get; set; }

    /// <summary>
    /// Identificador del producto vendido.
    /// Funciona como clave foránea hacia la entidad Producto.
    /// </summary>
    public int IdProducto { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia el producto asociado.
    /// Permite acceder a la información detallada del producto
    /// incluido en la venta.
    /// </summary>
    public Producto? Producto { get; set; }

    /// <summary>
    /// Cantidad de unidades del producto vendidas
    /// dentro de esta transacción.
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del producto al momento de la venta.
    /// Se almacena para conservar el historial de precios,
    /// incluso si el precio del producto cambia posteriormente.
    /// </summary>
    public decimal PrecioUnitario { get; set; }
}