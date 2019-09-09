using Microsoft.EntityFrameworkCore;
using Phonebook.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly PhonebookContext _context;

        public PersonService(PhonebookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Person>> GetPeopleAsync() =>
            await _context.People.ToListAsync();

        public Task<Models.Person> GetPersonByIdAsync(int personId) =>
            _context.People.FindAsync(personId);

        public async Task<bool> CreatePersonAsync(Models.Person person)
        {
            _context.People.Add(person);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdatePersonAsync(Models.Person person)
        {
            _context.People.Attach(person);
            _context.Entry(person).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            var person = new Models.Person { PersonId = personId, Deleted = true };
            _context.People.Attach(person);
            return await _context.SaveChangesAsync() == 1;
        }

    }
}
