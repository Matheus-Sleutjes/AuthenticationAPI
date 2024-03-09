using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Contract
{
    public interface IAuthenticationRepository
    {
        User Find(Guid id);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        void SaveChanges();
    }
}
