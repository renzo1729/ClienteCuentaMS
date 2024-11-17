using AccountTransactionService.Application.DTOs.Outputs;
using MediatR;

namespace AccountTransactionService.Application.Query
{
    public class GetTransactionByIdQuery : IRequest<TransactionRecordOutputDto>
    {
        public int TransactionId { get; }

        public GetTransactionByIdQuery(int transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
