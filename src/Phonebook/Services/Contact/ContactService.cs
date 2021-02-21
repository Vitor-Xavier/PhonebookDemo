using Phonebook.Exceptions;
using Phonebook.Repositories.Contact;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository) => _contactRepository = contactRepository;

        public async ValueTask<Models.Contact> GetContactById(int contactId, CancellationToken cancellationToken = default) =>
            await _contactRepository.GetById(contactId, cancellationToken);

        public IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId, CancellationToken cancellationToken = default) =>
            _contactRepository.GetContactsByPerson(personId);

        public async Task CreateContact(Models.Contact contact, CancellationToken cancellationToken = default)
        {
            if (!IsValid(contact)) throw new BadRequestException("Registro inválido");

            await _contactRepository.Add(contact, cancellationToken);
        }

        public async Task UpdateContact(int contatactId, Models.Contact contact, CancellationToken cancellationToken = default)
        {
            if (!IsValid(contact)) throw new BadRequestException("Registro inválido");
            contact.ContactId = contatactId;

            await _contactRepository.Edit(contact, cancellationToken);
        }

        public async Task DeleteContact(int contatactId, CancellationToken cancellationToken = default)
        {
            Models.Contact contact = new() { ContactId = contatactId, Deleted = true };

            await _contactRepository.Delete(contact, cancellationToken);
        }

        public bool IsValid(Models.Contact contact) => contact is { Text: { Length: > 0 }, PersonId: not 0, ContactTypeId: not 0 };
    }
}
