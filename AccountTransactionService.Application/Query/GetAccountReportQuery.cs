using AccountTransactionService.Application.DTOs.Inputs;
using AccountTransactionService.Application.DTOs.Outputs;
using MediatR;

namespace AccountTransactionService.Application.Query
{
    public class GetAccountReportQuery : IRequest<IEnumerable<AccountReportOutputDto>>
    {
        public AccountReportInputDto Input { get; }

        public GetAccountReportQuery(AccountReportInputDto input)
        {
            Input = input;
        }
    }
}
