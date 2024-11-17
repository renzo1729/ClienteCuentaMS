using MediatR;
using PersonClientService.Application.DTOs.Inputs;

namespace PersonClientService.Application.Command
{
    public class UpdateClientCommand : IRequest<bool>
    {
        public UpdateClientInputDto UpdateClientInputDto { get; set; }

        public UpdateClientCommand(UpdateClientInputDto updateClientInputDto)
        {
            UpdateClientInputDto = updateClientInputDto;
        }
    }
}
