using Phonebook.Repositories.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository) => _personRepository = personRepository;

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
            Models.Person person = new() { PersonId = personId, Deleted = true };

            await _personRepository.Delete(person);
        }

        public bool IsValid(Models.Person person) => person is { Name: { Length: > 0 }, UserId: not 0 };
    }
}
