using PersonClientService.Core.Domain.Entities;

namespace PersonClientService.Core.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetPersonByIdAsync(int id);
        Task<Person?> GetByIdentificationAsync(string identification);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
    }
}
