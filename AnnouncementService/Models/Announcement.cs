using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementService.Models
{
    public class Announcement
    {
        private EntityTypeBuilder<Announcement> entityTypeBuilder;

        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Announcement(Guid Id,int ClassId, string Title, string Message, DateTime Date )
        {
            this.Id = Id;
            this.Title = Title;
            this.Message = Message;
            this.Date = Date;
            this.ClassId = ClassId; 
        }

        public Announcement()
        {

        }

        public Announcement(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Announcement> entityTypeBuilder)
        {

        }
    }
}
