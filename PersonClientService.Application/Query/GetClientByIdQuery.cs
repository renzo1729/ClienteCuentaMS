using MediatR;
using PersonClientService.Application.DTOs.Outputs;

namespace PersonClientService.Application.Query
{
    public class GetClientByIdQuery : IRequest<ClientOutputDto>
    {
        public int ClientId { get; set; }

        public GetClientByIdQuery(int clientId)
        {
            ClientId = clientId;
        }
    }
}
