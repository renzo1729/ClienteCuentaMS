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
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateAccount([FromBody] CreateAccountInputDto createAccountInputDto)
        {
            var command = new CreateAccountCommand(createAccountInputDto);
            var accountId = await _mediator.Send(command);

            var response = new ApiResponse<int>(accountId);
            return CreatedAtAction(nameof(GetAccount), new { id = accountId }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AccountOutputDto>>> GetAccount(int id)
        {
            var query = new GetAccountByIdQuery(id);
            var account = await _mediator.Send(query);
            var response = new ApiResponse<AccountOutputDto>(account);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AccountOutputDto>>>> GetAllAccounts()
        {
            var query = new GetAllAccountsQuery();
            var accounts = await _mediator.Send(query);

            var response = new ApiResponse<IEnumerable<AccountOutputDto>>(accounts);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAccount(int id)
        {
            var command = new DeleteAccountCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
            {
                return NotFound(new ApiResponse<string>("Account not found."));
            }

            var response = new ApiResponse<bool>(success);
            return Ok(response);
        }
    }
}
