using Authentication.Domain.Entities;

namespace Authentication.Domain.Dto
{
    public class UserDto
    {
        public UserDto() { }
        public UserDto(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
