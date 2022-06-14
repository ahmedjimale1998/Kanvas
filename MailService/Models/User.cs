using System.ComponentModel.DataAnnotations;

namespace MailService.Models
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
    }
}
