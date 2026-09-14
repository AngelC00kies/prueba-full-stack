using FluentValidation;
using PruebaTecnica.Application.DTOs.Ventas;

namespace PruebaTecnica.Application.Validators;

/// <summary>
/// Validador encargado de verificar las reglas de negocio
/// y restricciones aplicables al proceso de creación de ventas.
/// Garantiza que la información recibida sea válida antes de
/// ser procesada por la capa de servicios.
/// </summary>
public class CrearVentaValidator : AbstractValidator<CrearVentaDto>
{
    /// <summary>
    /// Inicializa una nueva instancia del validador de ventas
    /// y define las reglas de validación para la entidad
    /// <see cref="CrearVentaDto"/> y sus detalles asociados.
    /// </summary>
    public CrearVentaValidator()
    {
        // Valida que se haya seleccionado un cliente válido.
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Debe seleccionar un cliente");

        // Valida que la venta contenga al menos un producto.
        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("La venta debe tener al menos un producto");

        // Aplica validaciones a cada detalle incluido en la venta.
        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            // Verifica que el identificador del producto sea válido.
            detalle.RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("Producto inválido");

            // Verifica que la cantidad solicitada sea mayor que cero.
            detalle.RuleFor(d => d.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");
        });
    }
}