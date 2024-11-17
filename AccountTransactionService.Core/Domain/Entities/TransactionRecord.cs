namespace AccountTransactionService.Core.Domain.Entities
{
    public class TransactionRecord
    {
        public int TransactionId { get; set; } 
        public DateTime Date { get; set; }
        public char TransactionType { get; set; } // Tipo de transacción (I: ingreso, O: salida)
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }

        // Propiedad de navegación hacia Account
        public Account Account { get; set; }
    }
}
