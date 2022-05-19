using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UserService.Data;
using UserService.Models;
using UserService.Repository;
using Xunit;

namespace UserServiceTest
{
    public class UserTest
    {
        private readonly UserContext _context;

        public UserTest()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("InMemoryUserDb");
            _context = new UserContext(optionsBuilder.Options);

        }

        [Fact]
        public void AddUser()
        {
            var userRepository = new UserRepository(_context);

            //Arrange
            var newUser = new User(Guid.NewGuid(),"Hans", "Hans@Gmail.com", "password", "Docent", 0);

            //Act
            var savedUser = userRepository.Add(newUser);

            //Assert
            Assert.NotNull(savedUser);
            Assert.Equal(newUser, savedUser.Result);
            Assert.Equal(newUser.Role, savedUser.Result.Role);
            Assert.Equal(newUser.Name, savedUser.Result.Name);
            Assert.Equal(newUser.Email, savedUser.Result.Email);

        }

        [Fact]
        public void GetUser()
        {
            var userRepository = new UserRepository(_context);

            //Arrange
            var newUser = new User(Guid.NewGuid(), "Hans", "Hans@Gmail.com", "password", "Docent", 0);
             
            //Act
            var savedUser = userRepository.Add(newUser);
            var getSavedUser = userRepository.Get(newUser.Id);

            //Assert
            Assert.NotNull(getSavedUser);
            Assert.Equal(getSavedUser.Result, savedUser.Result);

        }

        [Fact]
        public void GetAll()
        {
            var userRepository = new UserRepository(_context);

            //Arrange
            var newUser = new User(Guid.NewGuid(), "Hans", "Hans@Gmail.com", "password", "Docent", 0);

            //Act
            var savedMail = userRepository.Add(newUser);
            var getMails = userRepository.GetAllUsers();

            var mail = getMails.Result.FirstOrDefault(x => x.Id == newUser.Id);

            //Assert
            Assert.Equal(mail.Id, newUser.Id);
            Assert.Equal(mail.Role, newUser.Role);
            Assert.Equal(mail.Name, newUser.Name);
            Assert.Equal(mail.Password, newUser.Password);
        }

        [Fact]
        public void UpdateUser()
        {
            var userRepository = new UserRepository(_context);

            //Arrange
            var newUser = new User(Guid.NewGuid(), "Hans", "Hans@Gmail.com", "password", "Docent", 0);

            //Act
            var savedUser = userRepository.Add(newUser);
            newUser.Email = "hanspieter@gmail.com";
            _ = userRepository.Update(newUser);
            var updatedUser = userRepository.Get(newUser.Id);

            //Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(updatedUser.Result.Email, newUser.Email);
            Assert.Equal(newUser.Role, savedUser.Result.Role);
            Assert.Equal(newUser.Name, savedUser.Result.Name);
            Assert.Equal(newUser.Email, savedUser.Result.Email);

        }

        [Fact]
        public void Delete()
        {
            var userRepository = new UserRepository(_context);

            //Arrange
            var newUser = new User(Guid.NewGuid(), "Hans", "Hans@Gmail.com", "password", "Docent", 0);

            //Act
            var savedMail = userRepository.Add(newUser);
            _ = userRepository.Delete(savedMail.Result.Id);
            var getSavedMail = userRepository.Get(newUser.Id);


            //Assert
            Assert.Null(getSavedMail.Result.Name);
            Assert.Null(getSavedMail.Result.Email);
            Assert.Null(getSavedMail.Result.Password);
        }
    }
}
