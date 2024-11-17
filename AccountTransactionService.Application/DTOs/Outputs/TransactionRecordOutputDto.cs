
namespace AccountTransactionService.Application.DTOs.Outputs
{
    public class TransactionRecordOutputDto
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public char TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
    }
}
