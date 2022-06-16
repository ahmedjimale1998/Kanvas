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

        public Announcement(Guid _Id,int _ClassId, string _Title, string _Message, DateTime _Date )
        {
            Id = _Id;
            Title = _Title;
            Message = _Message;   
            Date = _Date;   
            ClassId = _ClassId; 
        }

        public Announcement()
        {

        }

        public Announcement(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Announcement> entityTypeBuilder)
        {

        }
    }
}
