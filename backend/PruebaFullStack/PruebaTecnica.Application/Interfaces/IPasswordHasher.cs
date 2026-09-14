namespace PruebaTecnica.Application.Interfaces;

/// <summary>
/// Define las operaciones para el manejo seguro de contraseñas.
/// Esta interfaz proporciona métodos para generar hashes de contraseñas
/// y verificar credenciales durante los procesos de autenticación.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Genera un hash seguro a partir de una contraseña en texto plano.
    /// El resultado debe ser almacenado en la base de datos en lugar
    /// de la contraseña original.
    /// </summary>
    /// <param name="password">
    /// Contraseña en texto plano que se desea proteger.
    /// </param>
    /// <returns>
    /// Cadena que representa el hash generado para la contraseña.
    /// </returns>
    string Hash(string password);

    /// <summary>
    /// Verifica si una contraseña en texto plano coincide con
    /// un hash almacenado previamente.
    /// </summary>
    /// <param name="password">
    /// Contraseña proporcionada por el usuario.
    /// </param>
    /// <param name="hash">
    /// Hash almacenado que será utilizado para la validación.
    /// </param>
    /// <returns>
    /// True si la contraseña es válida; False en caso contrario.
    /// </returns>
    bool Verify(string password, string hash);
}