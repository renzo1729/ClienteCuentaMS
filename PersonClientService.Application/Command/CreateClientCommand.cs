using MediatR;
using PersonClientService.Application.DTOs.Inputs;

namespace PersonClientService.Application.Command
{
    public class CreateClientCommand : IRequest<int>
    {
        public CreateClientInputDto CreateClientInputDto { get; set; }

        public CreateClientCommand(CreateClientInputDto createClientInputDto)
        {
            CreateClientInputDto = createClientInputDto;
        }
    }
}
