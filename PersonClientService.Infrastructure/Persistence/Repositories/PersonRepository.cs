using Microsoft.EntityFrameworkCore;
using PersonClientService.Core.Domain.Entities;
using PersonClientService.Core.Domain.Interfaces;
using PersonClientService.Infrastructure.Persistence.Context;


namespace PersonClientService.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task<Person?> GetByIdentificationAsync(string identification)
        {
            return await _context.Person.FirstOrDefaultAsync(p => p.Identification == identification);
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task AddPersonAsync(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
