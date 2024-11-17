using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Application.Query;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using AutoMapper;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountOutputDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper, IClientService clientService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<AccountOutputDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId) ?? throw new NotFoundException($"Cuenta con ID {request.AccountId} no encontrada.");
            var accountDto = _mapper.Map<AccountOutputDto>(account);
            var client = await _clientService.GetClientByIdAsync(accountDto.ClientId);
            if (client != null)
            {
                accountDto.ClientName = client.Name;
            }
            return accountDto;
        }
    }
}
