using System.ComponentModel.DataAnnotations;

namespace AccountTransactionService.Core.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; } 
        public string AccountNumber { get; set; } = string.Empty; 
        public string AccountType { get; set; } = string.Empty; 
        public decimal InitialBalance { get; set; } = 0.00m; 
        public bool Status { get; set; } = true; 
        public int ClientId { get; set; }
        // Propiedad de concurrencia
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public decimal CurrentBalance { get; set; }

        // Relación con TransactionRecord
        public ICollection<TransactionRecord> TransactionRecords { get; set; } = [];

    }
}
