using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public interface IPersonService
    {
        ValueTask<Models.Person> GetPersonById(int personId);

        Task<IEnumerable<Models.Person>> GetPeopleByUser(int userId);

        Task<bool> CreatePerson(Models.Person person);

        Task<bool> UpdatePerson(int personId, Models.Person person);

        Task<bool> DeletePerson(int personId);
    }
}
