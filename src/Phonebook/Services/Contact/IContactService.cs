using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public interface IContactService
    {
        ValueTask<Models.Contact> GetContactById(int contactId);

        IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId);

        Task CreateContact(Models.Contact contact);

        Task UpdateContact(int contatactId, Models.Contact contact);

        Task DeleteContact(int contatactId);
    }
}
