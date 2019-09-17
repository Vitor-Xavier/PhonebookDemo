using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public interface IContactTypeService
    {
        Task<IEnumerable<Models.ContactType>> GetContactTypes();

        Task<Models.ContactType> GetContactTypeById(int contactTypeId);

        Task<bool> CreateContactType(Models.ContactType contactType);

        Task<bool> UpdatContactType(Models.ContactType contactType);

        Task<bool> DeleteContactType(int contactTypeId);
    }
}
