using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace MailService.Models
{
    public class Mail
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverID { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }

        public Mail(Guid id, string senderId, string receiverID, string topic, string message, DateTime created)
        {
            this.Id = id;
            this.SenderId = senderId;
            this.ReceiverID = receiverID;
            this.Topic = topic;
            this.Message = message;
            this.Created = created;
        }

        public Mail() {}

        public Mail(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Mail> entityTypeBuilder)
        {

        }
       
    }
}
