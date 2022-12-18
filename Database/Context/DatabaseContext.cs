using Microsoft.EntityFrameworkCore;
using StoreAPI.Database_Context.EntitiesConfiguration;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database_Context
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DatabaseContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
        public DbSet<User> Users { get; set; }
    }
}
