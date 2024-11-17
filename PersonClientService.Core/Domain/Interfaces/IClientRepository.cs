using PersonClientService.Core.Domain.Entities;

namespace PersonClientService.Core.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetClientByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
    }
}
