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

        public void Validation()
        {
            if (Username.Length > 50) throw new Exception("Username ultrapassou 50 caracteres");
            if (Username.Length < 10) throw new Exception("Username tem que ter no minimo 10 caracteres");
            if (Email.Length > 50) throw new Exception("Email ultrapassou 255 caracteres");
            if (Email.Length < 10) throw new Exception("Email tem que ter no minimo 10 caracteres");
            if (PasswordHash == null) throw new Exception("Senha não pode ser vazio");
        }
    }
}
