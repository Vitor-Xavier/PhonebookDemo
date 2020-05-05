using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.ContactType;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
namespace Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypeController : ControllerBase
    {
        private readonly IContactTypeService _contactTypeService;

        public ContactTypeController(IContactTypeService contactTypeService) =>
            _contactTypeService = contactTypeService;

        /// <summary>
        /// Find a Contact Type by its identification.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <returns>Person</returns>
        [HttpGet("{contactTypeId:int}")]
        public ValueTask<ContactType> FindContactTypeById(int contactTypeId) =>
            _contactTypeService.GetContactTypeById(contactTypeId);

        /// <summary>
        /// Returns Contact Types.
        /// </summary>
        /// <returns>List of Contact Types</returns>
        [HttpGet]
        public Task<IEnumerable<ContactType>> GetContactTypes() =>
            _contactTypeService.GetContactTypes();

        /// <summary>
        /// Add a new Contact Type to the Phonebook.
        /// </summary>
        /// <param name="contactType">Contact Type data</param>
        /// <returns>Created Contact Type</returns>
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactType contactType)
        {
            await _contactTypeService.CreateContactType(contactType);
            return Created(nameof(ContactType), contactType);
        }

        /// <summary>
        /// Update an existing Contact Type.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <param name="contactType">Contact Type data</param>
        /// <returns>Updated Contact Type</returns>
        [HttpPut("{contactTypeId:int}")]
        public async Task<ActionResult<ContactType>> PutContactType(int contactTypeId, ContactType contactType)
        {
            await _contactTypeService.UpdatContactType(contactTypeId, contactType);
            return Ok(contactType);
        }

        /// <summary>
        /// Delete Contact Type by its identification.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactTypeId:int}")]
        public Task DeleteContactType(int contactTypeId) =>
            _contactTypeService.DeleteContactType(contactTypeId);
    }
}