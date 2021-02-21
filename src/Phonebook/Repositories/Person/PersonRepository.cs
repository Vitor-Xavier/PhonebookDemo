using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Repositories.Person
{
    public class PersonRepository : Repository<Models.Person, PhonebookContext>, IPersonRepository
    {
        public PersonRepository(PhonebookContext context) : base(context) { }

        public Task<List<Models.Person>> GetPeopleByUser(int userId, CancellationToken cancellationToken = default) =>
            _context.People.Where(person => person.UserId == userId).AsNoTracking().ToListAsync();
    }
}
