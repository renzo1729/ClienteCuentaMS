
using AccountTransactionService.Application.Command;
using AccountTransactionService.Core.Domain.Entities;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly ITransactionRecordRepository _transactionRecordRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionCommandHandler(ITransactionRecordRepository transactionRecordRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _transactionRecordRepository = transactionRecordRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Verificar existencia de la cuenta
                var account = await _accountRepository.GetAccountByIdAsync(request.Transaction.AccountId) ?? throw new NotFoundException($"Cuenta con id {request.Transaction.AccountId} no encontrada.");

                // Obtener la última transacción para validar el orden cronológico
                var lastTransaction = await _transactionRecordRepository.GetLastTransactionByAccountIdAsync(request.Transaction.AccountId);
                if (lastTransaction != null && request.Transaction.Date < lastTransaction.Date)
                {
                    throw new ValidationException("La fecha de la transacción no puede ser anterior a la última fecha de transacción.");
                }

                // Obtener el saldo actual de la última transacción
                var currentBalance = lastTransaction?.Balance ?? account.InitialBalance;

                // Calcular el nuevo saldo (suma normal ya que si es salida en monto sería negativo
                decimal newBalance = currentBalance + request.Transaction.Amount;

                // Verificar fondos suficientes
                if (newBalance < 0)
                {
                    throw new ValidationException("Saldo no disponible.");
                }

                // Crear y guardar la nueva transacción
                var newTransaction = new TransactionRecord
                {
                    Date = request.Transaction.Date,
                    TransactionType = request.Transaction.TransactionType,
                    Amount = request.Transaction.Amount,
                    Balance = newBalance,
                    AccountId = request.Transaction.AccountId
                };

                await _transactionRecordRepository.AddTransactionRecordAsync(newTransaction);
                // Actualizar el saldo actual en la cuenta
                account.CurrentBalance = newBalance;

                // Intentar actualizar la cuenta con control de concurrencia
                await _accountRepository.UpdateAccountAsync(account);
                await _unitOfWork.CommitTransactionAsync();

                return newTransaction.TransactionId;
            }
            catch (ConcurrencyException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new ConcurrencyException(ex.Message);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
