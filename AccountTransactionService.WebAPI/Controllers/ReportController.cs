using AccountTransactionService.Application.DTOs.Inputs;
using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Application.Query;
using AccountTransactionService.Core.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.WebAPI.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AccountReportOutputDto>>>> GetReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int clientId)
        {
            if (startDate > endDate)
            {
                return BadRequest(new ApiResponse<string>("La fecha de inicio no puede ser posterior a la fecha de fin."));
            }

            var inputDto = new AccountReportInputDto
            {
                ClientId = clientId,
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetAccountReportQuery(inputDto);
            var report = await _mediator.Send(query);

            if (!report.Any())
            {
                return NotFound(new ApiResponse<string>("No se encontraron datos para los criterios especificados."));
            }

            return Ok(new ApiResponse<IEnumerable<AccountReportOutputDto>>(report));
        }
    }
}
