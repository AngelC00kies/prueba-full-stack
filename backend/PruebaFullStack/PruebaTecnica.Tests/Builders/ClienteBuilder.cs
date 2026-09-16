using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Tests.Builders;

public class ClienteBuilder
{
    private int _id = 1;
    private string _nombre = "Cliente Test";
    private string _email = "cliente@test.com";
    private string _telefono = "555-1234";

    public ClienteBuilder ConId(int id) { _id = id; return this; }
    public ClienteBuilder ConNombre(string nombre) { _nombre = nombre; return this; }
    public ClienteBuilder ConEmail(string email) { _email = email; return this; }
    public ClienteBuilder ConTelefono(string telefono) { _telefono = telefono; return this; }

    public Cliente Build() => new()
    {
        Id = _id,
        Nombre = _nombre,
        Email = _email,
        Telefono = _telefono
    };
}