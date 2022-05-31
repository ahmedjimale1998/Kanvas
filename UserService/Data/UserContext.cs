using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
        }


        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new User(modelBuilder.Entity<User>());
            base.OnModelCreating(modelBuilder);
        }

    }
}
