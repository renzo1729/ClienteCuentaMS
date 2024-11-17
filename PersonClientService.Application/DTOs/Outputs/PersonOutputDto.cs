namespace PersonClientService.Application.DTOs.Outputs
{
    public class PersonOutputDto
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
