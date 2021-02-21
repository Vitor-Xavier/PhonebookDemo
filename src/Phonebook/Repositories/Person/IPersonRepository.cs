using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Repositories.Person
{
    public interface IPersonRepository : IRepository<Models.Person>
    {
        Task<List<Models.Person>> GetPeopleByUser(int userId, CancellationToken cancellationToken = default);
    }
}
