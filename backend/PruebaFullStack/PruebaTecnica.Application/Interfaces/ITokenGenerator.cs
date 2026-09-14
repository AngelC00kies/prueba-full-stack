using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces;

/// <summary>
/// Define las operaciones necesarias para la generación
/// de tokens de autenticación dentro del sistema.
/// Esta interfaz encapsula la lógica relacionada con la
/// creación de tokens utilizados para la autorización de usuarios.
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Genera un token de autenticación para el usuario especificado.
    /// El token contiene la información necesaria para identificar
    /// al usuario y validar sus permisos durante las solicitudes posteriores.
    /// </summary>
    /// <param name="usuario">
    /// Usuario para el cual se generará el token.
    /// </param>
    /// <returns>
    /// Cadena que representa el token generado.
    /// </returns>
    string GenerateToken(Usuario usuario);
}
