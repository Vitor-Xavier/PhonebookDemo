using Microsoft.Extensions.Logging;
using Phonebook.Repositories.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly ILogger _logger;

        private readonly IContactRepository _contactRepository;

        public ContactService(ILogger<ContactService> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

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
            var contact = new Models.Contact { ContactId = contatactId, Deleted = true };

            await _contactRepository.Delete(contact);
        }

        public bool IsValid(Models.Contact contact)
        {
            if (contact is null) return false;
            if (string.IsNullOrWhiteSpace(contact.Text)) return false;
            if (contact.PersonId is 0) return false;
            if (contact.ContactTypeId is 0) return false;

            _logger.LogInformation("Contact {0} is valid.", contact.Text);
            return true;
        }
    }
}
