using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Threading.Tasks;

namespace Phonebook.Services.User
{
    public class UserService : IUserService
    {
        private readonly PhonebookContext _context;

        public UserService(PhonebookContext context)
        {
            _context = context;
        }

        public async Task<Models.User> GetUserById(int userId) =>
            await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

        public async Task<bool> CreateUser(Models.User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateUser(Models.User user)
        {
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
    }
}
