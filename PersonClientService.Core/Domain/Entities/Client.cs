namespace PersonClientService.Core.Domain.Entities
{
    public class Client
    {
        public int ClientId { get; set; } // Clave primaria
        public int PersonId { get; set; } // Clave foránea

        public string Password { get; set; }
        public bool Status { get; set; }

        // Relación con Person
        public Person Person { get; set; }
    }
}
