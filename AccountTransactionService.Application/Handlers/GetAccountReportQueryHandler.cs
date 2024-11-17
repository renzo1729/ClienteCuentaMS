using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Application.Query;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class GetAccountReportQueryHandler : IRequestHandler<GetAccountReportQuery, IEnumerable<AccountReportOutputDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRecordRepository _transactionRecordRepository;
        private readonly IClientService _clientService;

        public GetAccountReportQueryHandler(IAccountRepository accountRepository, ITransactionRecordRepository transactionRecordRepository, IClientService clientService)
        {
            _accountRepository = accountRepository;
            _transactionRecordRepository = transactionRecordRepository;
            _clientService = clientService;
        }

        public async Task<IEnumerable<AccountReportOutputDto>> Handle(GetAccountReportQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAccountsByClientIdAsync(request.Input.ClientId);
            var client = await _clientService.GetClientByIdAsync(request.Input.ClientId) ?? throw new NotFoundException("Cliente no encontrado.");
            var report = new List<AccountReportOutputDto>();

            foreach (var account in accounts)
            {
                // Obtener la última transacción antes de la fecha de inicio
                var lastTransactionBeforeStartDate = await _transactionRecordRepository.GetLastTransactionBeforeDateAsync(
                    account.AccountId, request.Input.StartDate);

                decimal saldoInicial = lastTransactionBeforeStartDate?.Balance ?? account.InitialBalance;

                // Obtener las transacciones dentro del rango de fechas
                var endDateAdjusted = request.Input.EndDate.Date.AddDays(1).AddTicks(-1);
                var transactions = await _transactionRecordRepository.GetTransactionsByAccountIdAndDateRangeAsync(
                    account.AccountId, request.Input.StartDate, endDateAdjusted);

                decimal totalMovimiento = transactions.Sum(tr => tr.Amount);
                decimal saldoDisponible = saldoInicial + totalMovimiento;

                // Crear lista de detalles de movimientos
                var detalleMovimientos = transactions.Select(tr => new AccountReportDetailOutputDto
                {
                    Fecha = tr.Date.ToString("dd/MM/yyyy HH:mm:ss"),
                    TipoMovimiento = tr.TransactionType == 'I' ? "Ingreso" : "Salida",
                    Monto = tr.Amount,
                    Saldo = tr.Balance
                }).ToList();

                // Usar la fecha de la última transacción en el rango si existe
                string fechaUltimaTransaccion = transactions.Any() ? transactions.Max(tr => tr.Date).ToString("dd/MM/yyyy") : "-";

                report.Add(new AccountReportOutputDto
                {
                    Fecha = fechaUltimaTransaccion,
                    Cliente = client.Name,
                    NumeroCuenta = account.AccountNumber,
                    Tipo = account.AccountType,
                    SaldoInicial = saldoInicial,
                    Estado = account.Status,
                    Movimiento = totalMovimiento,
                    SaldoDisponible = saldoDisponible,
                    DetalleMovimientos = detalleMovimientos
                });
            }

            return report;
        }
    }
}
