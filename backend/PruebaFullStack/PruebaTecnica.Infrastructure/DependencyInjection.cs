using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Interfaces.Repositories;
using PruebaTecnica.Infrastructure.Data;
using PruebaTecnica.Infrastructure.Repositories;
using PruebaTecnica.Infrastructure.Security;

namespace PruebaTecnica.Infrastructure;

/// <summary>
/// Extensiones para registrar los servicios de la capa de infraestructura.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext con SQL Server
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositorios
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        // Seguridad
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}