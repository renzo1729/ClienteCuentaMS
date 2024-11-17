using AccountTransactionService.Application.DTOs.Inputs;
using FluentValidation;

namespace AccountTransactionService.Application.Validators
{
    public class CreateTransactionInputDtoValidator : AbstractValidator<CreateTransactionInputDto>
    {
        public CreateTransactionInputDtoValidator()
        {
            RuleFor(t => t.Date)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .Must(date => date <= DateTime.UtcNow)
                .WithMessage("La fecha de la transacción no puede estar en el futuro.");

            RuleFor(t => t.TransactionType)
                .Must(type => type == 'I' || type == 'O')
                .WithMessage("El tipo de transacción debe ser 'I' para ingreso o 'O' para egreso.");

            RuleFor(t => t.Amount)
                .NotEqual(0).WithMessage("El monto debe ser diferente de 0.");

            RuleFor(t => t.AccountId)
                .GreaterThan(0).WithMessage("El ID de la cuenta debe ser mayor que 0.");
        }
    }
}
