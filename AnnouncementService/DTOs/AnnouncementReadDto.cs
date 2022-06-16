namespace AnnouncementService.DTOs
{
    public class AnnouncementReadDto
    {
        public Guid Id { get; set; }
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
