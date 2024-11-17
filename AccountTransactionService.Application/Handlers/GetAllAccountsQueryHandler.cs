using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Application.Query;
using AccountTransactionService.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<AccountOutputDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper, IClientService clientService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<IEnumerable<AccountOutputDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            var accountDtos = _mapper.Map<IEnumerable<AccountOutputDto>>(accounts);

            foreach (var accountDto in accountDtos)
            {
                var client = await _clientService.GetClientByIdAsync(accountDto.ClientId);
                if (client != null)
                {
                    accountDto.ClientName = client.Name; 
                }
            }

            return accountDtos;
        }
    }
}
