using Phonebook.Repositories.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository) => _contactRepository = contactRepository;

        public async ValueTask<Models.Contact> GetContactById(int contactId) =>
            await _contactRepository.GetById(contactId);

        public IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId) =>
            _contactRepository.GetContactsByPerson(personId);

        public async Task CreateContact(Models.Contact contact)
        {
            if (!IsValid(contact)) return;

            await _contactRepository.Add(contact);
        }

        public async Task UpdateContact(int contatactId, Models.Contact contact)
        {
            if (!IsValid(contact)) return;
            contact.ContactId = contatactId;

            await _contactRepository.Edit(contact);
        }

        public async Task DeleteContact(int contatactId)
        {
            Models.Contact contact = new() { ContactId = contatactId, Deleted = true };

            await _contactRepository.Delete(contact);
        }

        public bool IsValid(Models.Contact contact) => contact is { Text: { Length: > 0 }, PersonId: not 0, ContactTypeId: not 0 };
    }
}
