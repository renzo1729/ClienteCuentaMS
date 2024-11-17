using AccountTransactionService.Core.Domain.Entities;


namespace AccountTransactionService.Core.Domain.Interfaces
{
    public interface ITransactionRecordRepository
    {
        Task<TransactionRecord?> GetTransactionRecordByIdAsync(int transactionId);
        Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByAccountIdAsync(int accountId);
        Task AddTransactionRecordAsync(TransactionRecord transactionRecord);
        Task UpdateTransactionRecordAsync(TransactionRecord transactionRecord);
        Task DeleteTransactionRecordAsync(int transactionId);
        Task<TransactionRecord?> GetLastTransactionByAccountIdAsync(int accountId);
        Task<IEnumerable<TransactionRecord>> GetTransactionsByAccountIdAndDateRangeAsync(int accountId, DateTime startDate, DateTime endDate);
        Task<TransactionRecord?> GetLastTransactionBeforeDateAsync(int accountId, DateTime date);
    }
}
