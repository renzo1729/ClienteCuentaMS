using AccountTransactionService.Core.Domain.Entities;

namespace AccountTransactionService.Core.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
        Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId);
    }
}
