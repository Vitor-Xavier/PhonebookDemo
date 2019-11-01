using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phonebook.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly ILogger _logger;

        private readonly PhonebookContext _context;

        public ContactService(ILogger<ContactService> logger, PhonebookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<Models.Contact> GetContactById(int contactId) =>
            _context.Contacts.AsNoTracking().SingleOrDefaultAsync(c => c.ContactId.Equals(contactId));

        public async Task<IEnumerable<Models.Contact>> GetContactsByPerson(int personId) =>
            await _context.Contacts.AsNoTracking().Where(c => c.PersonId == personId).AsNoTracking().ToListAsync();

        public async Task<bool> CreateContact(Models.Contact contact)
        {
            if (!IsValid(contact)) return false;

            _context.Contacts.Add(contact);
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> UpdateContact(int contatactId, Models.Contact contact)
        {
            if (!IsValid(contact)) return false;
            contact.ContactId = contatactId;

            _context.Contacts.Attach(contact);
            _context.Entry(contact).State = EntityState.Modified;
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> DeleteContact(int contatactId)
        {
            var contact = new Models.Contact { ContactId = contatactId, Deleted = true };
            _context.Contacts.Attach(contact);
            _context.Entry(contact).Property(c => c.Deleted).IsModified = true;
            return await _context.SaveChangesAsync() == 1;
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
