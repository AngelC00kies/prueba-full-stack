namespace PruebaTecnica.Application.DTOs.Ventas;

public class CrearVentaDto
{
    public int Id { get; set; }
    public List<CrearDetalleVentaDto> Detalles { get; set; } = new();
}

public class CrearDetalleVentaDto
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
}
