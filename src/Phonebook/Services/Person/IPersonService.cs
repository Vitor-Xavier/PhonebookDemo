using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public interface IPersonService
    {
        Task<Models.Person> GetPersonByIdAsync(int personId);

        Task<IEnumerable<Models.Person>> GetPeopleAsync();

        Task<bool> SavePersonAsync(Models.Person person);

        Task<bool> DeletePersonAsync(int personId);
    }
}
