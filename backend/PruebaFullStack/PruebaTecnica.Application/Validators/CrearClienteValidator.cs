using FluentValidation;
using PruebaTecnica.Application.DTOs.Clientes;

namespace PruebaTecnica.Application.Validators;

/// <summary>
/// Validador encargado de verificar las reglas de negocio
/// y restricciones aplicables al proceso de creación de clientes.
/// Utiliza FluentValidation para garantizar que la información
/// recibida sea válida antes de ser procesada por la aplicación.
/// </summary>
public class CrearClienteValidator : AbstractValidator<CrearClienteDto>
{
    /// <summary>
    /// Inicializa una nueva instancia del validador de clientes
    /// y define las reglas de validación para cada propiedad
    /// del objeto <see cref="CrearClienteDto"/>.
    /// </summary>
    public CrearClienteValidator()
    {
        // Valida que el nombre sea obligatorio
        // y no exceda la longitud máxima permitida.
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100);

        // Valida que el correo electrónico sea obligatorio,
        // tenga un formato válido y no exceda la longitud permitida.
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es válido")
            .MaximumLength(100);

        // Valida que el teléfono sea obligatorio
        // y que no supere la longitud máxima establecida.
        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio")
            .MaximumLength(20);
    }
}