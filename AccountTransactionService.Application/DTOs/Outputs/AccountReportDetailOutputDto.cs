
namespace AccountTransactionService.Application.DTOs.Outputs
{
    public class AccountReportDetailOutputDto
    {
        public string Fecha { get; set; } // Fecha de la transacción
        public string TipoMovimiento { get; set; } // Tipo de transacción (Ingreso/Salida)
        public decimal Monto { get; set; } 
        public decimal Saldo { get; set; }
    }
}
