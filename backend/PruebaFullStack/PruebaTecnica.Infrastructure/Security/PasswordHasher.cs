using PruebaTecnica.Application.Interfaces;

namespace PruebaTecnica.Infrastructure.Security;

/// <summary>
/// Implementación de hashing de contraseñas usando BCrypt.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    /// <inheritdoc />
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    /// <inheritdoc />
    public bool Verify(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}