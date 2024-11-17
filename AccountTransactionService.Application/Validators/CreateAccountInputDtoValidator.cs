using AccountTransactionService.Application.DTOs.Inputs;
using FluentValidation;

namespace AccountTransactionService.Application.Validators
{
    public class CreateAccountInputDtoValidator : AbstractValidator<CreateAccountInputDto>
    {
        public CreateAccountInputDtoValidator()
        {
            // Validación para AccountNumber
            RuleFor(account => account.AccountNumber)
                .NotEmpty().WithMessage("El número de cuenta es obligatorio.")
                .MaximumLength(20).WithMessage("El número de cuenta no debe exceder los 20 caracteres.");

            // Validación para AccountType
            RuleFor(account => account.AccountType)
                .NotEmpty().WithMessage("El tipo de cuenta es obligatorio.")
                .MaximumLength(50).WithMessage("El tipo de cuenta no debe exceder los 50 caracteres.");

            // Validación para InitialBalance
            RuleFor(account => account.InitialBalance)
                .GreaterThanOrEqualTo(0).WithMessage("El saldo inicial debe ser mayor o igual a 0.");

            // Validación para Status
            RuleFor(account => account.Status)
                .NotNull().WithMessage("El estado es obligatorio.");

            // Validación para ClientId
            RuleFor(account => account.ClientId)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor que 0.");
        }
    }
}
