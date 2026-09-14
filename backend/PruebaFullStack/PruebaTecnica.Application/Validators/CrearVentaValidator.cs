using FluentValidation;
using PruebaTecnica.Application.DTOs.Ventas;

namespace PruebaTecnica.Application.Validators;

public class CrearVentaValidator : AbstractValidator<CrearVentaDto>
{
    public CrearVentaValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Debe seleccionar un cliente");

        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("La venta debe tener al menos un producto");

        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            detalle.RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("Producto inválido");

            detalle.RuleFor(d => d.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");
        });
    }
}