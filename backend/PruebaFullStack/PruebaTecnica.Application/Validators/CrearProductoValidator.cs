using FluentValidation;
using PruebaTecnica.Application.DTOs.Productos;

namespace PruebaTecnica.Application.Validators;

public class CrearProductoValidator : AbstractValidator<CrearProductoDto>
{
    public CrearProductoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(30).WithMessage("El nombre no puede exceder 30 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es obligatoria")
            .MaximumLength(250).WithMessage("La descripción no puede exceder 250 caracteres");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
    }
}