namespace AccountTransactionService.Application.DTOs.Outputs
{
    public class AccountReportOutputDto
    {
        public string Fecha { get; set; } 
        public string Cliente { get; set; } 
        public string NumeroCuenta { get; set; } 
        public string Tipo { get; set; } 
        public decimal SaldoInicial { get; set; } 
        public bool Estado { get; set; } 
        public decimal Movimiento { get; set; } 
        public decimal SaldoDisponible { get; set; }

        public IEnumerable<AccountReportDetailOutputDto> DetalleMovimientos { get; set; } = new List<AccountReportDetailOutputDto>(); // Detalle de los movimientos
    }
}
