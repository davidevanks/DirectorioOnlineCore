
using FluentValidation;
using ModelApp.Models;

namespace ModelApp.Validator
{
    public class AnuncioInfoValidator : AbstractValidator<AnuncioInfo>
    {
        public AnuncioInfoValidator()
        {


            RuleFor(x => x.NombreNegocio)
                .NotNull().WithMessage("Debe escribir el nombre del negocio.")
                .MaximumLength(100).WithMessage("Maximo permitido es de 100 caracteres.");
            RuleFor(x => x.IdCategoria)
               .NotNull().WithMessage("Debe seleccionar una categoria.");

            RuleFor(x => x.DescripcionNegocio)
                .NotNull().WithMessage("Debe escribir la descripción del negocio.")
                .MaximumLength(500).WithMessage("Maximo permitido es de 500 caracteres.");
            RuleFor(x => x.IdPais)
               .NotNull().WithMessage("Debe seleccionar un pais.");
            RuleFor(x => x.IdDepartamento)
               .NotNull().WithMessage("Debe seleccionar un departamento.");
            RuleFor(x => x.DireccionNegocio)
               .NotNull().WithMessage("Debe escribir la dirección del negocio.")
               .MaximumLength(200).WithMessage("Maximo permitido es de 200 caracteres.");


        }
    }
}
