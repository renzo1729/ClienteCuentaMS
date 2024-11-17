using AccountTransactionService.Application.DTOs.Outputs;
using MediatR;

namespace AccountTransactionService.Application.Query
{
    public class GetAccountByIdQuery : IRequest<AccountOutputDto>
    {
        public int AccountId { get; set; }

        public GetAccountByIdQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
}
