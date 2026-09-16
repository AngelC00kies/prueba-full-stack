using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Tests.Builders;

/// <summary>
/// Builder para crear instancias de <see cref="Producto"/> con datos por defecto,
/// permitiendo sobrescribir propiedades específicas según el caso de prueba.
/// </summary>
public class ProductoBuilder
{
    private int _id = 1;
    private string _nombre = "Producto Test";
    private string _descripcion = "Descripción de prueba";
    private decimal _precio = 100m;
    private int _stock = 10;

    public ProductoBuilder ConId(int id)
    {
        _id = id;
        return this;
    }

    public ProductoBuilder ConNombre(string nombre)
    {
        _nombre = nombre;
        return this;
    }

    public ProductoBuilder ConPrecio(decimal precio)
    {
        _precio = precio;
        return this;
    }

    public ProductoBuilder ConStock(int stock)
    {
        _stock = stock;
        return this;
    }

    public Producto Build() => new()
    {
        Id = _id,
        Nombre = _nombre,
        Descripcion = _descripcion,
        Precio = _precio,
        Stock = _stock
    };
}