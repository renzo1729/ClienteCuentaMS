using AccountTransactionService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactionService.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Account> Account { get; set; }
        public DbSet<TransactionRecord> TransactionRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionRecord>(entity =>
            {
                entity.HasKey(tr => tr.TransactionId);
                entity.HasOne(tr => tr.Account)
                      .WithMany(a => a.TransactionRecords)
                      .HasForeignKey(tr => tr.AccountId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
