using AccountTransactionService.Application.Command;
using AccountTransactionService.Core.Domain.Entities;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using AutoMapper;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IClientService clientService, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            // Verificar la existencia del cliente a través del microservicio
            var client = await _clientService.GetClientByIdAsync(request.Account.ClientId);
            if (client == null)
            {
                throw new NotFoundException("Cliente no encontrado en el servicio externo.");
            }

            var account = _mapper.Map<Account>(request.Account);
            account.CurrentBalance = account.InitialBalance;
            await _accountRepository.AddAccountAsync(account);
            return account.AccountId;
        }
    }
}
