using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Repositories.ContactType
{
    public class ContactTypeRepository : Repository<Models.ContactType, PhonebookContext>, IContactTypeRepository
    {
        public ContactTypeRepository(PhonebookContext context) : base(context) { }

        public IAsyncEnumerable<Models.ContactType> GetContactTypesByName(string name) =>
            _context.ContactTypes.Where(contactType => name.Contains(contactType.Name)).AsNoTracking().AsAsyncEnumerable();
    }
}
