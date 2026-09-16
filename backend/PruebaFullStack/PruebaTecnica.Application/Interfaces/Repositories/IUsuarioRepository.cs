using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces.Repositories;

/// <summary>
/// Define las operaciones de acceso a datos para la entidad Usuario.
/// Esta interfaz establece el contrato que debe implementar cualquier
/// repositorio encargado de la gestión y persistencia de usuarios.
/// </summary>
public interface IUsuarioRepository
{
    /// <summary>
    /// Obtiene un usuario a partir de su nombre de usuario.
    /// Este método suele utilizarse durante los procesos de autenticación.
    /// </summary>
    /// <param name="username">
    /// Nombre de usuario que se desea buscar.
    /// </param>
    /// <returns>
    /// El usuario encontrado o null si no existe.
    /// </returns>
    Task<Usuario?> ObtenerPorUsernameAsync(string username);

    /// <summary>
    /// Crea un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">
    /// Entidad usuario que será almacenada.
    /// </param>
    /// <returns>
    /// El usuario creado con sus datos actualizados.
    /// </returns>
    Task<Usuario> CrearAsync(Usuario usuario);

    /// <summary>
    /// Verifica si ya existe un usuario registrado con el nombre
    /// de usuario especificado.
    /// </summary>
    /// <param name="username">
    /// Nombre de usuario que será validado.
    /// </param>
    /// <returns>
    /// True si el nombre de usuario ya existe;
    /// False en caso contrario.
    /// </returns>
    Task<bool> ExisteUsernameAsync(string username);
}