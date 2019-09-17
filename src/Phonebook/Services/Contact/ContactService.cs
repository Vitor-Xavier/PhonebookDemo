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

        public Task<Models.Contact> GetContactById(int contactId) =>
            _context.Contacts.AsNoTracking().SingleOrDefaultAsync(c => c.ContactId.Equals(contactId));

        public async Task<IEnumerable<Models.Contact>> GetContactsByPerson(int personId) =>
            await _context.Contacts.Where(c => c.PersonId == personId).AsNoTracking().ToListAsync();

        public async Task<bool> CreateContact(Models.Contact contact)
        {
            _context.Contacts.Add(contact);
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> UpdateContact(Models.Contact contact)
        {
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
    }
}
