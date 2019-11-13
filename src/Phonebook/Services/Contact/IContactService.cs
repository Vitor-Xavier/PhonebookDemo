using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public interface IContactService
    {
        ValueTask<Models.Contact> GetContactById(int contactId);

        IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId);

        Task<bool> CreateContact(Models.Contact contact);

        Task<bool> UpdateContact(int contatactId, Models.Contact contact);

        Task<bool> DeleteContact(int contatactId);
    }
}
