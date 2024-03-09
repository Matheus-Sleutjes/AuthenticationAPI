using Authentication.Domain.Entities;
using Authentication.Infrastructure.Context;
using Authentication.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AuthContext _context;
        public AuthenticationRepository(AuthContext context) 
        {
            _context = context;
        }

        public User Find(Guid id)
        {
            return _context.Users.AsNoTracking().Where(e => e.Id == id).SingleOrDefault();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
