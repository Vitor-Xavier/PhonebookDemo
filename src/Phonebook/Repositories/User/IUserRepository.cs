using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Repositories.User
{
    public interface IUserRepository : IRepository<Models.User>
    {
        Task<Models.User> GetUserByUsername(string username, CancellationToken cancellationToken = default);

        Task<bool> UsernameIsDefined(string username, CancellationToken cancellationToken = default);

        Task<Models.User> GetUserByUsernamePassword(string username, string password, CancellationToken cancellationToken = default);
    }
}
