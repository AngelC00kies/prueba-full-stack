namespace PruebaTecnica.Infrastructure.Security;

/// <summary>
/// Configuración para la generación de tokens JWT.
/// Se enlaza con la sección "JwtSettings" del appsettings.json.
/// </summary>
public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationHours { get; set; } = 2;
}