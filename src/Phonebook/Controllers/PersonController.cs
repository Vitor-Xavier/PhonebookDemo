using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.Person;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
namespace Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService) =>
            _personService = personService;

        /// <summary>
        /// Find a Person by its identification.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Person</returns>
        [HttpGet("{personId:int}")]
        public ValueTask<Person> FindPersonById(int personId, CancellationToken cancellationToken) =>
            _personService.GetPersonById(personId, cancellationToken);

        /// <summary>
        /// Returns People.
        /// </summary>
        /// <param name="userId">User identification</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>List of People</returns>
        [HttpGet("User/{userId:int}")]
        public Task<List<Person>> GetPeopleByUser(int userId, CancellationToken cancellationToken) =>
            _personService.GetPeopleByUser(userId, cancellationToken);

        /// <summary>
        /// Add a new Person to the Phonebook.
        /// </summary>
        /// <param name="person">Person data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Created Person</returns>
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person, CancellationToken cancellationToken)
        {
            await _personService.CreatePerson(person, cancellationToken);
            return Created(nameof(Person), person);
        }

        /// <summary>
        /// Update an existing Person.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <param name="person">Person data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Updated Person</returns>
        [HttpPut("{personId:int}")]
        public async Task<ActionResult<Person>> PutPerson(int personId, Person person, CancellationToken cancellationToken)
        {
            await _personService.UpdatePerson(personId, person, cancellationToken);
            return Ok(person);
        }

        /// <summary>
        /// Delete Person by its identification.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns></returns>
        [HttpDelete("{personId:int}")]
        public async Task<ActionResult> DeletePerson(int personId, CancellationToken cancellationToken)
        {
            await _personService.DeletePerson(personId, cancellationToken);
            return NoContent();
        }
    }
}