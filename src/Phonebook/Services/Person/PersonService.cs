using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phonebook.Context;
using Phonebook.Repositories.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly ILogger _logger;

        private readonly IPersonRepository _personRepository;

        public PersonService(ILogger<PersonService> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public Task<List<Models.Person>> GetPeopleByUser(int userId) =>
            _personRepository.GetPeopleByUser(userId);

        public ValueTask<Models.Person> GetPersonById(int personId) =>
            _personRepository.GetById(personId);

        public async Task CreatePerson(Models.Person person)
        {
            if (!IsValid(person)) return;

            await _personRepository.Add(person);
        }

        public async Task UpdatePerson(int personId, Models.Person person)
        {
            if (!IsValid(person)) return;
            person.PersonId = personId;

            await _personRepository.Edit(person);
        }

        public async Task DeletePerson(int personId)
        {
            var person = new Models.Person { PersonId = personId, Deleted = true };

            await _personRepository.Delete(person);
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
