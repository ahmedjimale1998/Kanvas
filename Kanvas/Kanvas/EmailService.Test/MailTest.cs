using MailService.Data;
using MailService.Models;
using MailService.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace EmailService.Test
{
    public class MailTest
    {
        private readonly MailContext _context;

        public MailTest()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("InMemoryMailDb");
            _context = new MailContext(optionsBuilder.Options);
            
        }

        [Fact]
        public void AddMail()
        {
            var mailRepository = new MailRepository(_context);

            //Arrange
            var newMail = new Mail(Guid.NewGuid(), "1233", "1324", "topic", "message", DateTime.Now);

            //Act
            var savedMail = mailRepository.Add(newMail);

            //Assert
            Assert.NotNull(savedMail);
            Assert.Equal(newMail, savedMail.Result);

        }

        [Fact]
        public void GetMail()
        {
            var mailRepository = new MailRepository(_context);

            //Arrange
            var newMail = new Mail(Guid.NewGuid(), "1233", "1324", "topic", "message", DateTime.Now);

            //Act
            var savedMail = mailRepository.Add(newMail);
            var getSavedMail = mailRepository.Get(newMail.Id);

            //Assert
            Assert.NotNull(getSavedMail);
            Assert.Equal(getSavedMail.Result, savedMail.Result);

        }

        [Fact]
        public void GetAll()
        {
            var mailRepository = new MailRepository(_context);

            //Arrange
            var newMail = new Mail(Guid.NewGuid(), "1233", "1233", "topic", "message", DateTime.Now);

            //Act
            var savedMail = mailRepository.Add(newMail);
            var getMails = mailRepository.GetAll();

            //Assert
            Assert.Single(getMails.Result);
            Assert.Collection(getMails.Result, x => Assert.Contains("1233", x.SenderId));
            Assert.Collection(getMails.Result, x => Assert.Contains("1233", x.ReceiverID));
            Assert.Collection(getMails.Result, x => Assert.Contains("topic", x.Topic));
        }

        [Fact]
        public void Delete()
        {
            var mailRepository = new MailRepository(_context);

            //Arrange
            var newMail = new Mail(Guid.NewGuid(), "1233", "1324", "topic", "message", DateTime.Now);

            //Act
            var savedMail = mailRepository.Add(newMail);
            _ = mailRepository.Delete(savedMail.Result.Id);
            var getSavedMail = mailRepository.Get(newMail.Id);


            //Assert
            Assert.Null(getSavedMail.Result.Message);
            Assert.Null(getSavedMail.Result.ReceiverID);
            Assert.Null(getSavedMail.Result.SenderId);
        }
    }
}