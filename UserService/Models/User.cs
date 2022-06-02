using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }   // ? LA

        [Required]
        public string? Password { get; set; } // ? LA

        public string? Role { get; set; }

        public int? ClassId { get; set; }




        public User(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> entityTypeBuilder)
        {

        }
        public User()
        {

        }

        public User(Guid id, string Name, string Email, string Password, string Role, int ClassId)
        {
            Id = id;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Role = Role;
            this.ClassId = ClassId;
        }

    }

    /* Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> entityTypeBuilder*/
}