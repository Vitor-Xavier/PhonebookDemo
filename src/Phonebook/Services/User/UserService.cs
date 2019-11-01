using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phonebook.Context;
using System.Threading.Tasks;

namespace Phonebook.Services.User
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;

        private readonly PhonebookContext _context;

        public UserService(ILogger<UserService> logger, PhonebookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Models.User> GetUserById(int userId) =>
            await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == userId);

        public async Task<Models.User> GetUserByUsername(string username) =>
            await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username.Equals(username));

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
            var user = new Models.User { UserId = userId, Deleted = true };
            _context.Users.Attach(user);
            return await _context.SaveChangesAsync() == 1;
        }

        public bool IsValid(Models.User user)
        {
            if (user is null) return false;
            if (string.IsNullOrWhiteSpace(user.Username)) return false;
            if (string.IsNullOrWhiteSpace(user.Email)) return false;
            if (string.IsNullOrWhiteSpace(user.Name)) return false;

            _logger.LogInformation("User {0} is valid.", user.Username);
            return true;
        }
    }
}
