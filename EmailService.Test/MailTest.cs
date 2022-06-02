using MailService.Data;
using MailService.Models;
using MailService.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

            var mail = getMails.Result.FirstOrDefault(x => x.Id == newMail.Id);

            //Assert
            Assert.Equal(mail.Id, newMail.Id);
            Assert.Equal(mail.Message, newMail.Message);
            Assert.Equal(mail.ReceiverID, newMail.ReceiverID);
            Assert.Equal(mail.SenderId, newMail.SenderId);
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