using AccountTransactionService.Core.Domain.Entities;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using AccountTransactionService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactionService.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Account
                                 .Where(a => a.Status == true)
                                 .ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _context.Account
                                 .Include(a => a.TransactionRecords)
                                 .FirstOrDefaultAsync(a => a.AccountId == accountId && a.Status == true); 
        }

        public async Task AddAccountAsync(Account account)
        {
            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            try
            {
                _context.Account.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("The account was modified by another process. Please reload and try again.", ex);
            }
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account != null)
            {
                account.Status = false;
                _context.Account.Update(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId)
        {
            return await _context.Account
                                 .Where(a => a.ClientId == clientId && a.Status == true)
                                 .ToListAsync();
        }
    }
}
