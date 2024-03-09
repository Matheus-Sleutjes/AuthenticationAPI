using Authentication.Application.Contract;
using Authentication.Domain.Dto;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Contract;

namespace Authentication.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        public UserDto Find(Guid id)
        {
            var entity = _authenticationRepository.Find(id);

            if (entity == null) throw new Exception("Não existe entidade com esse id");

            return new UserDto(entity);
        }

        public void Add(UserDto user)
        {
            var entity = new User(user.Username, user.Password);
            _authenticationRepository.Add(entity);
            _authenticationRepository.SaveChanges();
        }

        public UserDto Update(UserDto user, Guid id)
        {
            var entity = _authenticationRepository.Find(id);

            if (entity == null) throw new Exception("Não existe entidade com esse id");

            entity.Username = user.Username;
            entity.Password = user.Password;

            _authenticationRepository.Update(entity);
            _authenticationRepository.SaveChanges();
            return user;
        }

        public void Delete(Guid id)
        {
            var entity = _authenticationRepository.Find(id);

            if (entity == null) throw new Exception("Não existe entidade com esse id");

            _authenticationRepository.Delete(entity);
            _authenticationRepository.SaveChanges();
        }
    }
}
