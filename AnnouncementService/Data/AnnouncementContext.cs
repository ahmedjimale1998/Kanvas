using AnnouncementService.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementService.Data
{
    public class AnnouncementContext : DbContext
    {
        public AnnouncementContext() { }

        public AnnouncementContext(DbContextOptions options) : base(options){ }

        public DbSet<Announcement> Announcement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Announcement(modelBuilder.Entity<Announcement>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
