namespace PersonClientService.Application.DTOs.Inputs
{
    public class UpdateClientInputDto
    {
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public char? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Identification { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }
    }
}
