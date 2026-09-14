namespace PruebaTecnica.Application.DTOs.Ventas;

public class VentaDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int IdCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public List<DetalleVentaDto> Detalles { get; set; } = new();

}
