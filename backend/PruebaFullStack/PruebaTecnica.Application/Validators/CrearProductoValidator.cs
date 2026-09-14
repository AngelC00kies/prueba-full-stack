using FluentValidation;
using PruebaTecnica.Application.DTOs.Productos;

namespace PruebaTecnica.Application.Validators;

/// <summary>
/// Validador encargado de verificar las reglas de negocio
/// y restricciones aplicables al proceso de creación de productos.
/// Utiliza FluentValidation para garantizar que los datos recibidos
/// cumplan con los requisitos establecidos antes de ser procesados.
/// </summary>
public class CrearProductoValidator : AbstractValidator<CrearProductoDto>
{
    /// <summary>
    /// Inicializa una nueva instancia del validador de productos
    /// y define las reglas de validación para cada propiedad
    /// del objeto <see cref="CrearProductoDto"/>.
    /// </summary>
    public CrearProductoValidator()
    {
        // Valida que el nombre del producto sea obligatorio
        // y no exceda la cantidad máxima de caracteres permitida.
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(30).WithMessage("El nombre no puede exceder 30 caracteres");

        // Valida que la descripción sea obligatoria
        // y que no supere la longitud máxima definida.
        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es obligatoria")
            .MaximumLength(250).WithMessage("La descripción no puede exceder 250 caracteres");

        // Valida que el precio sea mayor que cero,
        // asegurando que el producto tenga un valor válido.
        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        // Valida que la cantidad disponible en inventario
        // no sea un número negativo.
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
    }
}