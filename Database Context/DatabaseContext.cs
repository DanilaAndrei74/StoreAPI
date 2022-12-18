using Microsoft.EntityFrameworkCore;
using StoreAPI.Database_Entities;

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
        public DbSet<User> Users { get; set; }
    }
}
