using MediatR;
namespace AccountTransactionService.Application.Command
{
    public class DeleteAccountCommand : IRequest<bool>
    {
        public int AccountId { get; set; }

        public DeleteAccountCommand(int accountId)
        {
            AccountId = accountId;
        }
    }
}
