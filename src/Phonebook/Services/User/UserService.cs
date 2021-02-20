using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Phonebook.Common;
using Phonebook.Context;
using Phonebook.Exceptions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Services.User
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;

        private readonly AppSettings _appSettings;

        private readonly PhonebookContext _context;

        public UserService(ILogger<UserService> logger, IOptions<AppSettings> appSettings, PhonebookContext context)
        {
            _logger = logger;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public ValueTask<Models.User> GetUserById(int userId) =>
            _context.Users.FindAsync(userId);

        public async Task<Models.User> GetUserByUsername(string username) =>
            await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username.Equals(username));

        public async ValueTask<Models.User> Authenticate(string username, string password)
        {
            if (await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password && !u.Deleted) is Models.User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.UserId.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = null;

                return user;
            }
            _logger.LogInformation($"Authentication failed for user '{username}' at {DateTime.Now}");
            throw new NotFoundException("Username or password is incorrect");
        }

        public async Task<bool> CreateUser(Models.User user)
        {
            if (!IsValid(user)) return false;

            _context.Users.Add(user);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateUser(int userId, Models.User user)
        {
            if (!IsValid(user)) return false;
            user.UserId = userId;

            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            Models.User user = new() { UserId = userId, Deleted = true };
            _context.Users.Attach(user);
            return await _context.SaveChangesAsync() == 1;
        }

        public bool IsValid(Models.User user) => user is { Username: { Length: > 0 }, Email: { Length: > 0 }, Name: { Length: > 0 } };
    }
}
