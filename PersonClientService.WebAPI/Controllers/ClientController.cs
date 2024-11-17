using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonClientService.Application.Command;
using PersonClientService.Application.DTOs.Inputs;
using PersonClientService.Application.DTOs.Outputs;
using PersonClientService.Application.Query;
using PersonClientService.Core.Shared.Response;

namespace PersonClientService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateClient([FromBody] CreateClientInputDto createClientInputDto)
        {
            var command = new CreateClientCommand(createClientInputDto);
            var clientId = await _mediator.Send(command);

            var response = new ApiResponse<int>(clientId);
            return CreatedAtAction(nameof(GetClient), new { id = clientId }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ClientOutputDto>>> GetClient(int id)
        {
            var query = new GetClientByIdQuery(id);
            var client = await _mediator.Send(query);

            if (client == null)
            {
                return NotFound(new ApiResponse<string>("Client not found."));
            }

            var response = new ApiResponse<ClientOutputDto>(client);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ClientOutputDto>>>> GetAllClients()
        {
            var query = new GetAllClientsQuery();
            var clients = await _mediator.Send(query);

            var response = new ApiResponse<IEnumerable<ClientOutputDto>>(clients);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateClient([FromBody] UpdateClientInputDto updateClientInputDto)
        {
            var command = new UpdateClientCommand(updateClientInputDto);
            var success = await _mediator.Send(command);

            if (!success)
            {
                return NotFound(new ApiResponse<string>("Client not found."));
            }

            var response = new ApiResponse<bool>(success);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteClient(int id)
        {
            var command = new DeleteClientCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
            {
                return NotFound(new ApiResponse<string>("Client not found."));
            }

            var response = new ApiResponse<bool>(success);
            return Ok(response);
        }
    }
}
