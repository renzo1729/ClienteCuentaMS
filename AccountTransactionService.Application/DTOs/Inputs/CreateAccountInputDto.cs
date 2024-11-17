
namespace AccountTransactionService.Application.DTOs.Inputs
{
    public class CreateAccountInputDto
    {
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal InitialBalance { get; set; } = 0.00m;
        public bool Status { get; set; } = true;
        public int ClientId { get; set; }
    }
}
