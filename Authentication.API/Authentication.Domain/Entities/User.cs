using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class User : Entity
    {
        public User(string username, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
