using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Tests.Builders;

public class UsuarioBuilder
{
    private int _id = 1;
    private string _username = "testuser";
    private string _passwordHash = "hashed_password";
    private string _rol = "user";
    private bool _activo = true;
    private DateTime _fechaCreacion = DateTime.UtcNow;

    public UsuarioBuilder ConId(int id) { _id = id; return this; }
    public UsuarioBuilder ConUsername(string username) { _username = username; return this; }
    public UsuarioBuilder ConPasswordHash(string hash) { _passwordHash = hash; return this; }
    public UsuarioBuilder ConRol(string rol) { _rol = rol; return this; }
    public UsuarioBuilder ConActivo(bool activo) { _activo = activo; return this; }

    public Usuario Build() => new()
    {
        Id = _id,
        Username = _username,
        PasswordHash = _passwordHash,
        Rol = _rol,
        Activo = _activo,
        FechaCreacion = _fechaCreacion
    };
}