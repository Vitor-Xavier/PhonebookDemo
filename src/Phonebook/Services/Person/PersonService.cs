using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phonebook.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly ILogger _logger;

        private readonly PhonebookContext _context;

        public PersonService(ILogger<PersonService> logger, PhonebookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<Models.Person>> GetPeopleByUser(int userId) =>
            await _context.People.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();

        public ValueTask<Models.Person> GetPersonById(int personId) =>
            _context.People.FindAsync(personId);

        public async Task<bool> CreatePerson(Models.Person person)
        {
            if (!IsValid(person)) return false;

            _context.People.Add(person);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdatePerson(int personId, Models.Person person)
        {
            if (!IsValid(person)) return false;
            person.PersonId = personId;

            _context.People.Attach(person);
            _context.Entry(person).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePerson(int personId)
        {
            var person = new Models.Person { PersonId = personId, Deleted = true };
            _context.People.Attach(person);
            return await _context.SaveChangesAsync() == 1;
        }

        public bool IsValid(Models.Person person)
        {
            if (person is null) return false;
            if (string.IsNullOrWhiteSpace(person.Name)) return false;
            if (person.BirthDate == default) return false;

            _logger.LogInformation("Person {0} is valid.", person.Name);
            return true;
        }
    }
}
