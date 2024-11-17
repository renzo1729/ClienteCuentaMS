using PersonClientService.Core.Domain.Entities;
using PersonClientService.Core.Domain.Interfaces;
using PersonClientService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace PersonClientService.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _context.Client
                .Include(c => c.Person) // Incluye la relación con Person
                .FirstOrDefaultAsync(c => c.ClientId == id);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Client
                .Include(c => c.Person) // Incluye la relación con Person
                .ToListAsync();
        }

        public async Task AddClientAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Client.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
