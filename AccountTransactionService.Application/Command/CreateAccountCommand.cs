using AccountTransactionService.Application.DTOs.Inputs;
using MediatR;


namespace AccountTransactionService.Application.Command
{
    public class CreateAccountCommand : IRequest<int>
    {
        public CreateAccountInputDto Account { get; set; }

        public CreateAccountCommand(CreateAccountInputDto account)
        {
            Account = account;
        }
    }
}
