using MediatR;

namespace PersonClientService.Application.Command
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int ClientId { get; set; }

        public DeleteClientCommand(int clientId)
        {
            ClientId = clientId;
        }
    }
}
