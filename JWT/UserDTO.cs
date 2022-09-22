﻿namespace JWT
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        public bool Verified { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
