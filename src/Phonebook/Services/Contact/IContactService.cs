using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public interface IContactService
    {
        Task<Models.Contact> GetContactById(int contactId);

        Task<IEnumerable<Models.Contact>> GetContactsByPerson(int personId);

        Task<bool> CreateContact(Models.Contact contact);

        Task<bool> UpdateContact(Models.Contact contact);

        Task<bool> DeleteContact(int contatactId);
    }
}
