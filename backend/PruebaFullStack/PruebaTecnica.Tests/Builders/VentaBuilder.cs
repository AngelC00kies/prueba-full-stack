using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Tests.Builders;

public class VentaBuilder
{
    private int _id = 1;
    private int _idCliente = 1;
    private decimal _total = 100m;
    private List<DetalleVenta> _detalles = new();

    public VentaBuilder ConId(int id) { _id = id; return this; }
    public VentaBuilder ConIdCliente(int idCliente) { _idCliente = idCliente; return this; }
    public VentaBuilder ConTotal(decimal total) { _total = total; return this; }
    public VentaBuilder ConDetalles(List<DetalleVenta> detalles) { _detalles = detalles; return this; }

    public Venta Build() => new()
    {
        Id = _id,
        Fecha = DateTime.UtcNow,
        IdCliente = _idCliente,
        Total = _total,
        Detalles = _detalles
    };
}