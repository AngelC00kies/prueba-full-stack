using FluentValidation;
using PruebaTecnica.Application.DTOs.Auth;

namespace PruebaTecnica.Application.Validators;

/// <summary>
/// Validador encargado de verificar las credenciales enviadas
/// durante el proceso de inicio de sesión.
/// Garantiza que la información requerida cumpla con los criterios
/// mínimos antes de ser procesada por el servicio de autenticación.
/// </summary>
public class LoginValidator : AbstractValidator<LoginDto>
{
    /// <summary>
    /// Inicializa una nueva instancia del validador de inicio de sesión
    /// y define las reglas de validación para el objeto
    /// <see cref="LoginDto"/>.
    /// </summary>
    public LoginValidator()
    {
        // Valida que el nombre de usuario sea obligatorio.
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El username es obligatorio");

        // Valida que la contraseña sea obligatoria
        // y tenga una longitud mínima permitida.
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
    }
}