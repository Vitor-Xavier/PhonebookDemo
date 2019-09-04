using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Services.Person;
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
        public ActionResult<Task> FindPersonById(int personId) =>
            _personService.GetPersonByIdAsync(personId);

        /// <summary>
        /// Returns People.
        /// </summary>
        /// <returns>List of People</returns>
        [HttpGet]
        public ActionResult<Task> GetPeople() =>
            _personService.GetPeopleAsync();

        /// <summary>
        /// Add a new Person to the Phonebook.
        /// </summary>
        /// <param name="person">Person data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Task> CreatePerson(Models.Person person) =>
            _personService.SavePersonAsync(person);

        /// <summary>
        /// Update an existing Person.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <param name="person">Person data</param>
        /// <returns></returns>
        [HttpPut("{personId:int}")]
        public ActionResult<Task> UpdatePerson(int personId, Models.Person person) =>
            _personService.SavePersonAsync(person);

        /// <summary>
        /// Delete Person by its identification.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <returns></returns>
        [HttpDelete("{personId:int}")]
        public ActionResult<Task> DeletePerson(int personId) =>
            _personService.DeletePersonAsync(personId);
    }
}