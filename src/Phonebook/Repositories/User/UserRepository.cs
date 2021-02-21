using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Repositories.User
{
    public class UserRepository : Repository<Models.User, PhonebookContext>, IUserRepository
    {
        public UserRepository(PhonebookContext context) : base(context) { }

        public Task<Models.User> GetUserByUsername(string username, CancellationToken cancellationToken = default) =>
            _context.Users.Where(user => user.Username == username).AsNoTracking().SingleOrDefaultAsync(cancellationToken);

        public Task<Models.User> GetUserByUsernamePassword(string username, string password, CancellationToken cancellationToken = default) =>
            _context.Users.Where(user => user.Username == username && user.Password == password && !user.Deleted).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
    }
}
