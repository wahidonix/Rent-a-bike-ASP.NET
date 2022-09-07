namespace JWT
{
    public class Staff
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }

        public static implicit operator Staff(User v)
        {
            throw new NotImplementedException();
        }
    }
}
