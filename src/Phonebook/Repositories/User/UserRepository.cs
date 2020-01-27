using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Repositories.User
{
    public class UserRepository : Repository<Models.User, PhonebookContext>, IUserRepository
    {
        public UserRepository(PhonebookContext context) : base(context) { }

        public Task<List<Models.User>> GetUserByUsername(string username) =>
            _context.Users.Where(user => user.Username == username).AsNoTracking().ToListAsync();
    }
}
