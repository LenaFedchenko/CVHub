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
    }
}
