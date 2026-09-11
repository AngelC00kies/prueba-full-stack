using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Entities;

public class DetalleVenta
{
    public int Id { get; set; }
    public int IdVenta { get; set; }
    public Venta? Venta { get; set; }
    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }


}
