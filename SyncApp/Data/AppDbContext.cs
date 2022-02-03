using Microsoft.EntityFrameworkCore;
using SyncApp.Models;

namespace SyncApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Platform> Platform { get; set; }
        public DbSet<Well> Well { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Well>().Property(e => e.Id).ValueGeneratedNever();
        }
    }
}
