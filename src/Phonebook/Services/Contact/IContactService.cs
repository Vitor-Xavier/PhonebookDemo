using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public interface IContactService
    {
        Task<Models.Contact> GetContactByIdAsync(int contactId);

        Task<IEnumerable<Models.Contact>> GetContactsByPersonAsync(int personId);

        Task<bool> CreateContactAsync(Models.Contact contact);

        Task<bool> UpdateContactAsync(Models.Contact contact);

        Task<bool> DeleteContactAsync(int contatactId);
    }
}
