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

            User model = User.Create(user.Id, user.Email, user.Password, user.DateOfReg,
                user.IsConfirmed, new List<Guid>(), user.ConfirmationCode).Model;

            return new ModelWrapper<string>(GenerateToken(model), string.Empty, true);
        }

        public async Task<ModelWrapper<string>> SignUp(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if(user != null)
                return new ModelWrapper<string>(string.Empty, "A user with this email already exists", false);

            var hash = GenerateHash(password);

            int code = new Random().Next(1000, 9999);

            var model = User.Create(Guid.NewGuid(), email, password, 
                DateTime.Now, false, new List<Guid>(), code);

            if (!model.IsValid)
                return new ModelWrapper<string>(string.Empty, model.ErrorMessage, false);

            var entity = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = hash,
                DateOfReg = DateTime.Now,
                ConfirmationCode = code,
                IsConfirmed = false,
                Orders = new List<OrderEntity>()
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ModelWrapper<string>(code.ToString(), string.Empty, true);
        }

        public List<User> GetUsers()
        {
            var entities = _context.Users
                .Include(x => x.Orders)
                .ToList();

            return entities.Select(x => User.Create(x.Id, x.Email, x.Password,
                x.DateOfReg, x.IsConfirmed,
                x.Orders.Select(x => x.Id).ToList(), x.ConfirmationCode).Model).ToList();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(x => x.Orders)
                .SingleAsync(x => x.Id == id);

            return User.Create(user.Id, user.Email, user.Password,
                user.DateOfReg, user.IsConfirmed,
                user.Orders.Select(x => x.Id).ToList(), user.ConfirmationCode).Model;
        }
        public async Task<Guid> ConfirmUser(int code)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstAsync(x => x.ConfirmationCode == code);

            user.IsConfirmed = true;
            user.ConfirmationCode = 0;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}