using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
namespace Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Find a Person by its identification.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <returns>Person</returns>
        [HttpGet("{personId:int}")]
        public ValueTask<Person> FindPersonById(int personId) =>
            _personService.GetPersonById(personId);

        /// <summary>
        /// Returns People.
        /// </summary>
        /// <returns>List of People</returns>
        [HttpGet("User/{userId:int}")]
        public IAsyncEnumerable<Person> GetPeopleByUser(int userId) =>
            _personService.GetPeopleByUser(userId);

        /// <summary>
        /// Add a new Person to the Phonebook.
        /// </summary>
        /// <param name="person">Person data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> PostPerson(Person person) =>
            Created(nameof(Person), await _personService.CreatePerson(person));

        /// <summary>
        /// Update an existing Person.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <param name="person">Person data</param>
        /// <returns></returns>
        [HttpPut("{personId:int}")]
        public Task<bool> PutPerson(int personId, Person person) =>
            _personService.UpdatePerson(personId, person);

        /// <summary>
        /// Delete Person by its identification.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <returns></returns>
        [HttpDelete("{personId:int}")]
        public Task<bool> DeletePerson(int personId) =>
             _personService.DeletePerson(personId);
    }
}