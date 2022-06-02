namespace MailService.DTOs
{
    public class MailReadDto
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverID { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}
