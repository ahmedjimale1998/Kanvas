using AnnouncementService.Data;
using AnnouncementService.Models;
using AnnouncementService.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace AnnouncementService.Test
{
    public class AnnouncementTest
    {
        private readonly AnnouncementContext _context;

        public AnnouncementTest()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("InMemoryAnnouncementDb");
            _context = new AnnouncementContext(optionsBuilder.Options);
        }

        [Fact]
        public void AddAnnouncementTest()
        {
            var announcementRepository = new AnnouncementRepository(_context);

            //Arrange
            var newAnnouncement = new Announcement(Guid.NewGuid(), 1, "1324", "topic" ,DateTime.Now);

            //Act
            var savedAnnouncement = announcementRepository.Add(newAnnouncement);

            //Assert
            Assert.NotNull(savedAnnouncement);
            Assert.Equal(newAnnouncement, savedAnnouncement.Result);

        }
    }
}