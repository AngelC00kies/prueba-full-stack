using System.Net;
using System.Text.Json;
using FluentValidation;

namespace PruebaTecnica.API.Middleware;

/// <summary>
/// Middleware global para capturar excepciones no controladas
/// y devolver respuestas JSON claras al cliente.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Error de validación");
            await WriteResponse(context, HttpStatusCode.BadRequest, new
            {
                mensaje = "Errores de validación",
                errores = ex.Errors.Select(e => new { campo = e.PropertyName, error = e.ErrorMessage })
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Acceso no autorizado");
            await WriteResponse(context, HttpStatusCode.Unauthorized, new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado");
            await WriteResponse(context, HttpStatusCode.InternalServerError, new
            {
                mensaje = "Ocurrió un error inesperado en el servidor",
                detalle = ex.Message
            });
        }
    }

    private static async Task WriteResponse(HttpContext context, HttpStatusCode status, object body)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var json = JsonSerializer.Serialize(body, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}