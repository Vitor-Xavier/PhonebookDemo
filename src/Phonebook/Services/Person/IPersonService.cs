using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public interface IPersonService
    {
        Task<Models.Person> GetPersonByIdAsync(int personId);

        Task<IEnumerable<Models.Person>> GetPeopleAsync();

        Task<bool> CreatePersonAsync(Models.Person person);

        Task<bool> UpdatePersonAsync(Models.Person person);

        Task<bool> DeletePersonAsync(int personId);
    }
}
