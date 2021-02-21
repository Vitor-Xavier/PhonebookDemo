using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public interface IPersonService
    {
        ValueTask<Models.Person> GetPersonById(int personId, CancellationToken cancellationToken = default);

        Task<List<Models.Person>> GetPeopleByUser(int userId, CancellationToken cancellationToken = default);

        Task CreatePerson(Models.Person person, CancellationToken cancellationToken = default);

        Task UpdatePerson(int personId, Models.Person person, CancellationToken cancellationToken = default);

        Task DeletePerson(int personId, CancellationToken cancellationToken = default);
    }
}
