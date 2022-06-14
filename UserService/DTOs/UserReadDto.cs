namespace UserService.DTOs
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int ClassId { get; set; }
         
        /* public int Id { get; set; }
         public string Name { get; set; }
         public string Publisher { get; set; }
         public string Cost { get; set; }*/
    }
}
    