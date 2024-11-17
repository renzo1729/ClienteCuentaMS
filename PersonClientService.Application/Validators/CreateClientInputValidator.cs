using FluentValidation;
using PersonClientService.Application.DTOs.Inputs;

namespace PersonClientService.Application.Validators
{
    public class CreateClientInputValidator : AbstractValidator<CreateClientInputDto>
    {
        public CreateClientInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Gender)
                .Must(g => g == 'M' || g == 'F').WithMessage("El género debe ser 'M' o 'F'.")
                .When(x => !string.IsNullOrEmpty(x.Gender.ToString()));

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe estar en el pasado.");

            RuleFor(x => x.Identification)
                .NotEmpty().WithMessage("La identificación es obligatoria.")
                .MaximumLength(50).WithMessage("La identificación no puede exceder los 50 caracteres.");

            RuleFor(x => x.Address)
                .MaximumLength(255).WithMessage("La dirección no puede exceder los 255 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("El teléfono no puede exceder los 20 caracteres.")
                .Matches(@"^\d+$").WithMessage("El teléfono solo debe contener números.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(4).WithMessage("La contraseña debe tener al menos 4 caracteres.")
                .MaximumLength(255).WithMessage("La contraseña no puede exceder los 255 caracteres.");
        }
    }

}
