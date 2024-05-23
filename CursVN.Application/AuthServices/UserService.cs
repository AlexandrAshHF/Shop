using CursVN.Core;
using CursVN.Core.Abstractions.AuthServices;
using CursVN.Core.Models;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CursVN.Application.AuthServices
{
    public class UserService : IUserService
    {
        private ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        private string GenerateHash(string pass)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(pass);
        }
        private bool ComparePassHash(string pass, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(pass, hash);
        }
        private string GenerateToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString())];

            var signingCredentials = new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(AuthOptions.ExpiresHours)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
        public async Task<ModelWrapper<string>> LogIn(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return new ModelWrapper<string>(string.Empty, "No user with this email was found", false);

            if(!ComparePassHash(password, user.Password))
                return new ModelWrapper<string>(string.Empty, "Wrong password", false);

            User model = User.Create(user.Id, user.Email, user.Password, new List<Guid>()).Model;

            return new ModelWrapper<string>(GenerateToken(model), string.Empty, true);
        }

        public async Task<ModelWrapper<string>> SignUp(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if(user != null)
                return new ModelWrapper<string>(string.Empty, "A user with this email already exists", false);

            var hash = GenerateHash(password);

            var model = User.Create(Guid.NewGuid(), email, password, new List<Guid>());

            if (!model.IsValid)
                return new ModelWrapper<string>(string.Empty, model.ErrorMessage, false);

            var entity = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = hash,
                Orders = new List<OrderEntity>()
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ModelWrapper<string>(GenerateToken(model.Model), string.Empty, true);
        }

        public List<User> GetUsers()
        {
            var entities = _context.Users
                .Include(x => x.Orders)
                .ToList();

            return entities.Select(x => User.Create(x.Id, x.Email, x.Password,
                x.Orders.Select(x => x.Id).ToList()).Model).ToList();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(x => x.Orders)
                .SingleAsync(x => x.Id == id);

            return User.Create(user.Id, user.Email, user.Password,
                user.Orders.Select(x => x.Id).ToList()).Model;
        }
    }
}