using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public interface IContactTypeService
    {
        Task<IEnumerable<Models.ContactType>> GetContactTypes();

        ValueTask<Models.ContactType> GetContactTypeById(int contactTypeId);

        Task CreateContactType(Models.ContactType contactType);

        Task UpdatContactType(int contactTypeId, Models.ContactType contactType);

        Task DeleteContactType(int contactTypeId);
    }
}
