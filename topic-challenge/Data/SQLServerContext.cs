using Microsoft.EntityFrameworkCore;
using topic_challenge.Models;

namespace topic_challenge.Data
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Topic>? Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Topics)
                .WithOne(t => t.User);
        }
    }
}
