using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Repositories.User
{
    public interface IUserRepository : IRepository<Models.User>
    {
        Task<List<Models.User>> GetUserByUsername(string username);
    }
}
