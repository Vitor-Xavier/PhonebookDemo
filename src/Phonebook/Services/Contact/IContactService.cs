using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public interface IContactService
    {
        ValueTask<Models.Contact> GetContactById(int contactId, CancellationToken cancellationToken = default);

        IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId, CancellationToken cancellationToken = default);

        Task CreateContact(Models.Contact contact, CancellationToken cancellationToken = default);

        Task UpdateContact(int contatactId, Models.Contact contact, CancellationToken cancellationToken = default);

        Task DeleteContact(int contatactId, CancellationToken cancellationToken = default);
    }
}
