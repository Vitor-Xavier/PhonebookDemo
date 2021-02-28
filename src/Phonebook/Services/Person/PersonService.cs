using Phonebook.Exceptions;
using Phonebook.Repositories.Person;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository) => _personRepository = personRepository;

        public Task<List<Models.Person>> GetPeopleByUser(int userId, CancellationToken cancellationToken = default) =>
            _personRepository.GetPeopleByUser(userId, cancellationToken);

        public ValueTask<Models.Person> GetPersonById(int personId, CancellationToken cancellationToken = default) =>
            _personRepository.GetById(personId, cancellationToken);

        public async Task CreatePerson(Models.Person person, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!IsValid(person)) throw new BadRequestException("Registro inválido");

            await _personRepository.Add(person, cancellationToken);
        }

        public async Task UpdatePerson(int personId, Models.Person person, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!IsValid(person)) throw new BadRequestException("Registro inválido");
            person.PersonId = personId;

            await _personRepository.Edit(person, cancellationToken);
        }

        public async Task DeletePerson(int personId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Models.Person person = new() { PersonId = personId, Deleted = true };

            await _personRepository.Delete(person, cancellationToken);
        }

        public bool IsValid(Models.Person person) => person is { Name: { Length: > 0 }, BirthDate: { Year: > 1900 }, UserId: not 0 };
    }
}
