using MediatR;
using PersonClientService.Application.DTOs.Outputs;

namespace PersonClientService.Application.Query
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientOutputDto>> { }
}
