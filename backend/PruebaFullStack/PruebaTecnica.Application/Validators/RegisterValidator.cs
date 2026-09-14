using FluentValidation;
using PruebaTecnica.Application.DTOs.Auth;

namespace PruebaTecnica.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El username es obligatorio")
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        RuleFor(x => x.Rol)
            .NotEmpty()
            .Must(r => r == "user" || r == "admin")
            .WithMessage("El rol debe ser 'user' o 'admin'");
    }
}