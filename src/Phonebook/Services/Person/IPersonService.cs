using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public interface IPersonService
    {
        ValueTask<Models.Person> GetPersonById(int personId);

        Task<List<Models.Person>> GetPeopleByUser(int userId);

        Task CreatePerson(Models.Person person);

        Task UpdatePerson(int personId, Models.Person person);

        Task DeletePerson(int personId);
    }
}
