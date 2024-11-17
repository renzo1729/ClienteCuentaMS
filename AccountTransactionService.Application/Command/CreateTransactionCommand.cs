using AccountTransactionService.Application.DTOs.Inputs;
using MediatR;

namespace AccountTransactionService.Application.Command
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public CreateTransactionInputDto Transaction { get; }

        public CreateTransactionCommand(CreateTransactionInputDto transaction)
        {
            Transaction = transaction;
        }
    }
}
