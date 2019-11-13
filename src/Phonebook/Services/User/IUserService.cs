using System.Threading.Tasks;

namespace Phonebook.Services.User
{
    public interface IUserService
    {
        ValueTask<Models.User> GetUserById(int userId);

        Task<Models.User> GetUserByUsername(string username);

        Task<bool> CreateUser(Models.User user);

        Task<bool> UpdateUser(int userId, Models.User user);

        Task<bool> DeleteUser(int userId);
    }
}
