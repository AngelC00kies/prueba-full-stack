using FluentValidation;
using PruebaTecnica.Application.DTOs.Auth;

namespace PruebaTecnica.Application.Validators;

/// <summary>
/// Validador encargado de verificar la información recibida
/// durante el proceso de registro de nuevos usuarios.
/// Define las reglas necesarias para garantizar que los datos
/// cumplan con los requisitos mínimos de seguridad y consistencia.
/// </summary>
public class RegisterValidator : AbstractValidator<RegisterDto>
{
    /// <summary>
    /// Inicializa una nueva instancia del validador de registro
    /// y configura las reglas de validación para el objeto
    /// <see cref="RegisterDto"/>.
    /// </summary>
    public RegisterValidator()
    {
        // Valida que el nombre de usuario sea obligatorio,
        // tenga una longitud mínima de 3 caracteres y
        // no supere los 50 caracteres.
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El username es obligatorio")
            .MinimumLength(3)
            .MaximumLength(50);

        // Valida que la contraseña sea obligatoria y
        // tenga al menos 6 caracteres para cumplir
        // con los requisitos mínimos de seguridad.
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        // Valida que el rol sea obligatorio y que
        // únicamente se permitan los valores autorizados.
        RuleFor(x => x.Rol)
            .NotEmpty()
            .Must(r => r == "user" || r == "admin")
            .WithMessage("El rol debe ser 'user' o 'admin'");
    }
}