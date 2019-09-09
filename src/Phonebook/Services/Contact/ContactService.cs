using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly PhonebookContext _context;

        public ContactService(PhonebookContext context)
        {
            _context = context;
        }

        public Task<Models.Contact> GetContactByIdAsync(int contactId) =>
            _context.Contacts.FindAsync(contactId);

        public async Task<IEnumerable<Models.Contact>> GetContactsByPersonAsync(int personId) =>
            await _context.Contacts.Where(c => c.PersonId == personId).ToListAsync();

        public async Task<bool> CreateContactAsync(Models.Contact contact)
        {
            _context.Contacts.Add(contact);
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> UpdateContactAsync(Models.Contact contact)
        {
            _context.Contacts.Add(contact);
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> DeleteContactAsync(int contatactId)
        {
            var contact = new Models.Contact { ContactId = contatactId, Deleted = true };
            _context.Contacts.Attach(contact);
            _context.Entry(contact).Property(c => c.Deleted).IsModified = true;
            return await _context.SaveChangesAsync() == 1;
        }
    }
}
