using MailService.Models;
using Microsoft.EntityFrameworkCore;

namespace MailService.Data
{
    public class MailContext : DbContext
    {
        /*public MailContext(DbContextOptions<MailContext> options) : base(options) { }*/

        public MailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Mail> Mail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Mail(modelBuilder.Entity<Mail>());
            base.OnModelCreating(modelBuilder);
        }
    }
}



