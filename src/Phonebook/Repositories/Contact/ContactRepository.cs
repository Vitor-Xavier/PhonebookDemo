using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Repositories.Contact
{
    public class ContactRepository : Repository<Models.Contact, PhonebookContext>, IContactRepository
    {
        public ContactRepository(PhonebookContext context) : base(context) { }

        public IAsyncEnumerable<Models.Contact> GetContactsByType(int contactTypeId) =>
            _context.Contacts.Where(contact => contact.ContactTypeId == contactTypeId).AsNoTracking().AsAsyncEnumerable();

        public IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId) => 
            _context.Contacts.Where(c => c.PersonId == personId).AsAsyncEnumerable();
    }
}
