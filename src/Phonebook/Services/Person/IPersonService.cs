using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public interface IPersonService
    {
        Task<Models.Person> GetPersonById(int personId);

        Task<IEnumerable<Models.Person>> GetPeople();

        Task<bool> CreatePerson(Models.Person person);

        Task<bool> UpdatePerson(Models.Person person);

        Task<bool> DeletePerson(int personId);
    }
}
