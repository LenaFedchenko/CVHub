
using Microsoft.EntityFrameworkCore;
using CVHub.Models; 

namespace CVHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Registration> Users { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Account)
                .WithOne(a => a.Registration)
                .HasForeignKey<Account>(a => a.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}