using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Entities;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public int IdCliente { get; set; }
    public Cliente? Cliente { get; set; }
    public List<DetalleVenta> Detalles { get; set; } = new();
    public decimal Total => Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
}
