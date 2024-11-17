
namespace AccountTransactionService.Application.DTOs.Outputs
{
    public class AccountOutputDto
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal InitialBalance { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
    }
}
