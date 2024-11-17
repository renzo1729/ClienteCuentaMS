namespace AccountTransactionService.Application.DTOs.Inputs
{
    public class AccountReportInputDto
    {
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
