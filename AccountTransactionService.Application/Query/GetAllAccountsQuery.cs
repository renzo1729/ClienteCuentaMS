using AccountTransactionService.Application.DTOs.Outputs;
using MediatR;

namespace AccountTransactionService.Application.Query
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<AccountOutputDto>>
    {
    }
}
