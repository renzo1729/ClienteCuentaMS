using AccountTransactionService.Application.Command;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRecordRepository _transactionRecordRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository, ITransactionRecordRepository transactionRecordRepository)
        {
            _accountRepository = accountRepository;
            _transactionRecordRepository = transactionRecordRepository;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId) ?? throw new NotFoundException($"Cuenta con id {request.AccountId} no encontrada.");
            var lastTransaction = await _transactionRecordRepository.GetLastTransactionByAccountIdAsync(request.AccountId);
            if (lastTransaction != null && lastTransaction.Balance != 0)
            {
                throw new ValidationException($"La cuenta con id {request.AccountId} no puede ser desactivada porque el saldo de la última transacción no es 0.");
            }
            account.Status = false;
            await _accountRepository.DeleteAccountAsync(account.AccountId);
            return true;
        }
    }
}
