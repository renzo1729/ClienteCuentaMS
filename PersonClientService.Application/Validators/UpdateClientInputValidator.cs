using FluentValidation;
using PersonClientService.Application.DTOs.Inputs;

namespace PersonClientService.Application.Validators
{
    public class UpdateClientInputValidator : AbstractValidator<UpdateClientInputDto>
    {
        public UpdateClientInputValidator()
        {
            RuleFor(x => x.ClientId)
                .GreaterThan(0).WithMessage("El ClientId debe ser mayor que 0.");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Gender)
                .Must(g => g == 'M' || g == 'F').WithMessage("El género debe ser 'M' o 'F'.")
                .When(x => x.Gender.HasValue);

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe estar en el pasado.")
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.Identification)
                .MaximumLength(50).WithMessage("La identificación no puede exceder los 50 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Identification));

            RuleFor(x => x.Address)
                .MaximumLength(255).WithMessage("La dirección no puede exceder los 255 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("El teléfono no puede exceder los 20 caracteres.")
                .Matches(@"^\d+$").WithMessage("El teléfono solo debe contener números.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Password)
                .MinimumLength(4).WithMessage("La contraseña debe tener al menos 4 caracteres.")
                .MaximumLength(255).WithMessage("La contraseña no puede exceder los 255 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
