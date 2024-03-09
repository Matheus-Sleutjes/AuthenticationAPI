using Authentication.Domain.Dto;

namespace Authentication.Application.Contract
{
    public interface IAuthenticationService
    {
        UserDto Find(Guid id);
        void Add(UserDto user);
        UserDto Update(UserDto user, Guid id);
        void Delete(Guid id);
        string Login(UserDto request);
    }
}
