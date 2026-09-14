namespace PruebaTecnica.Domain.Entities;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public int IdCliente { get; set; }
    public Cliente? Cliente { get; set; }
    public decimal Total { get; set; }                              // ← con set
    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}