﻿namespace UserService.DTOs
{
    public class AddUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int ClassId { get; set; }

    }
}
