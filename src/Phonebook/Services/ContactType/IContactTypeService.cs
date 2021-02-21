using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public interface IContactTypeService
    {
        Task<IEnumerable<Models.ContactType>> GetContactTypes(CancellationToken cancellationToken = default);

        ValueTask<Models.ContactType> GetContactTypeById(int contactTypeId, CancellationToken cancellationToken = default);

        Task CreateContactType(Models.ContactType contactType, CancellationToken cancellationToken = default);

        Task UpdatContactType(int contactTypeId, Models.ContactType contactType, CancellationToken cancellationToken = default);

        Task DeleteContactType(int contactTypeId, CancellationToken cancellationToken = default);
    }
}
