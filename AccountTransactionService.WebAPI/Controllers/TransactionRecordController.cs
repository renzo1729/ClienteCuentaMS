using AccountTransactionService.Application.Command;
using AccountTransactionService.Application.DTOs.Inputs;
using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Application.Query;
using AccountTransactionService.Core.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateTransaction([FromBody] CreateTransactionInputDto createTransactionInputDto)
        {
            var command = new CreateTransactionCommand(createTransactionInputDto);
            var transactionId = await _mediator.Send(command);

            var response = new ApiResponse<int>(transactionId);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transactionId }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TransactionRecordOutputDto>>> GetTransactionById(int id)
        {
            var query = new GetTransactionByIdQuery(id);
            var transactionRecord = await _mediator.Send(query);
            var response = new ApiResponse<TransactionRecordOutputDto>(transactionRecord);
            return Ok(response);
        }


    }
}
