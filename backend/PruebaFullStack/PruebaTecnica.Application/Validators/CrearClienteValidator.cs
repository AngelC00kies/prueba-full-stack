using FluentValidation;
using PruebaTecnica.Application.DTOs.Clientes;

namespace PruebaTecnica.Application.Validators;

public class CrearClienteValidator : AbstractValidator<CrearClienteDto>
{
    public CrearClienteValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es válido")
            .MaximumLength(100);

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio")
            .MaximumLength(20);
    }
}