using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.User
{
    public interface IUserService
    {
        ValueTask<Models.User> GetUserById(int userId, CancellationToken cancellationToken = default);

        Task<Models.User> GetUserByUsername(string username, CancellationToken cancellationToken = default);

        ValueTask<Models.User> Authenticate(string username, string password, CancellationToken cancellationToken = default);

        Task CreateUser(Models.User user, CancellationToken cancellationToken = default);

        Task UpdateUser(int userId, Models.User user, CancellationToken cancellationToken = default);

        Task DeleteUser(int userId, CancellationToken cancellationToken = default);
    }
}
