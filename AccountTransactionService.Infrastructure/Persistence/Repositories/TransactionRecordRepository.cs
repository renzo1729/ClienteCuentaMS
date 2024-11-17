using AccountTransactionService.Core.Domain.Entities;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactionService.Infrastructure.Persistence.Repositories
{
    public class TransactionRecordRepository : ITransactionRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionRecord?> GetTransactionRecordByIdAsync(int transactionId)
        {
            return await _context.TransactionRecord
                                 .Include(tr => tr.Account)
                                 .FirstOrDefaultAsync(tr => tr.TransactionId == transactionId);
        }

        public async Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByAccountIdAsync(int accountId)
        {
            return await _context.TransactionRecord
                                 .Where(tr => tr.AccountId == accountId)
                                 .ToListAsync();
        }

        public async Task AddTransactionRecordAsync(TransactionRecord transactionRecord)
        {
            await _context.TransactionRecord.AddAsync(transactionRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionRecordAsync(TransactionRecord transactionRecord)
        {
            _context.TransactionRecord.Update(transactionRecord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionRecordAsync(int transactionId)
        {
            var transactionRecord = await GetTransactionRecordByIdAsync(transactionId);
            if (transactionRecord != null)
            {
                _context.TransactionRecord.Remove(transactionRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TransactionRecord?> GetLastTransactionByAccountIdAsync(int accountId)
        {
            return await _context.TransactionRecord
                                 .Where(tr => tr.AccountId == accountId)
                                 .OrderByDescending(tr => tr.Date)
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TransactionRecord>> GetTransactionsByAccountIdAndDateRangeAsync(int accountId, DateTime startDate, DateTime endDate)
        {
            return await _context.TransactionRecord
                                 .Where(tr => tr.AccountId == accountId && tr.Date >= startDate && tr.Date <= endDate)
                                 .OrderBy(tr => tr.Date)
                                 .ToListAsync();
        }

        public async Task<TransactionRecord?> GetLastTransactionBeforeDateAsync(int accountId, DateTime date)
        {
            return await _context.TransactionRecord
                                 .Where(tr => tr.AccountId == accountId && tr.Date < date)
                                 .OrderByDescending(tr => tr.Date)
                                 .FirstOrDefaultAsync();
        }

    }
}
