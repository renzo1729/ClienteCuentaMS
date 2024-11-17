using Microsoft.EntityFrameworkCore;
using PersonClientService.Core.Domain.Entities;


namespace PersonClientService.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación uno a uno entre Person y Client
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Person)
                .WithOne(p => p.Client)
                .HasForeignKey<Client>(c => c.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Elimina el cliente si la persona se elimina
        }
    }
}
