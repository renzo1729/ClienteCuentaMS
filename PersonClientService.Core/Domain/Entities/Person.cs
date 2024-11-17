namespace PersonClientService.Core.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; set; } // Clave primaria
        public string Name { get; set; }
        public char Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Identification { get; set; } // Clave única
        public string Address { get; set; }
        public string Phone { get; set; }

        // Relación con Client (uno a uno)
        public Client Client { get; set; }
    }
}
