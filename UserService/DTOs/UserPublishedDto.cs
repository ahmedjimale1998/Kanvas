namespace UserService.DTOs
{
    public class UserPublishedDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int ClassId { get; set; }
        public string Event { get; set; }   

    }
}
