using Authentication.Application.Contract;
using Authentication.Domain.Dto;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Contract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string CerealKey = "=-erIs1|L9zrvXqN9}5bdI{OXS1UZa^?X{/Re-/v>#RqN9}5bdI{OerIs1|L9zrvXqN9>#RqN9}5bdI{OerIs1|L9zrv";
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

        public Guid Add(UserDto user)
        {
            var userExist = _authenticationRepository.Find(user.Username);
            if (userExist != null) throw new Exception($"{user.Username} ja existe!");

            Guid id;
            using (var hmac = new HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                var entity = new User(user.Username, user.Email, passwordHash, passwordSalt);
                id = entity.Id;

                entity.Validation();
                _authenticationRepository.Add(entity);
            }
            _authenticationRepository.SaveChanges();
            return id;
        }

        public UserDto Update(UserDto user, Guid id)
        {
            var entity = _authenticationRepository.Find(id);
            if (entity == null) throw new Exception("Não existe entidade com esse id");

            using (var hmac = new HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            }
            entity.Username = user.Username;
            entity.Email = user.Email;

            entity.Validation();

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

        public string Login(LoginDto request)
        {
            var user = _authenticationRepository.Find(request.Username);
            if (user == null) throw new Exception("Usuario não Existe!");

            if (!VerifyPasswordHash(request.Password, user)) throw new Exception("Senha Incorreta!");

            var token = CreateToken(user);

            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CerealKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(string passwordRequest, User user)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordRequest));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}
