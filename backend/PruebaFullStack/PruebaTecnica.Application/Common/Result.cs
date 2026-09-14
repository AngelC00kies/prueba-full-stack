namespace PruebaTecnica.Application.Common;

/// <summary>
/// Representa el resultado de una operación dentro de la aplicación.
/// Permite indicar si la operación fue exitosa, proporcionar un mensaje
/// descriptivo y, opcionalmente, devolver datos asociados al resultado.
/// </summary>
/// <typeparam name="T">
/// Tipo de dato que será retornado cuando la operación se complete exitosamente.
/// </typeparam>
public class Result<T>
{
    /// <summary>
    /// Indica si la operación se ejecutó correctamente.
    /// True cuando la operación fue exitosa; False en caso contrario.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje descriptivo del resultado de la operación.
    /// Puede contener información de éxito, advertencia o error.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Datos retornados por la operación.
    /// Su valor será nulo cuando la operación falle o no exista información que devolver.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Crea y devuelve un resultado exitoso.
    /// </summary>
    /// <param name="data">Información resultante de la operación.</param>
    /// <param name="message">
    /// Mensaje descriptivo de éxito.
    /// Por defecto: "Operacion exitosa".
    /// </param>
    /// <returns>
    /// Instancia de <see cref="Result{T}"/> indicando éxito.
    /// </returns>
    public static Result<T> Ok(T data, string message = "Operacion exitosa")
        => new()
        {
            Success = true,
            Message = message,
            Data = data
        };

    /// <summary>
    /// Crea y devuelve un resultado fallido.
    /// </summary>
    /// <param name="message">
    /// Mensaje que describe la causa del error o fallo ocurrido.
    /// </param>
    /// <returns>
    /// Instancia de <see cref="Result{T}"/> indicando error.
    /// </returns>
    public static Result<T> Fail(string message)
        => new()
        {
            Success = false,
            Message = message
        };
}