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

        public ContactTypeController(IContactTypeService contactTypeService)
        {
            _contactTypeService = contactTypeService;
        }

        /// <summary>
        /// Find a Contact Type by its identification.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <returns>Person</returns>
        [HttpGet("{contactTypeId:int}")]
        public async Task<ContactType> FindContactTypeById(int contactTypeId) =>
            await _contactTypeService.GetContactTypeById(contactTypeId);

        /// <summary>
        /// Returns Contact Types.
        /// </summary>
        /// <returns>List of Contact Types</returns>
        [HttpGet]
        public async Task<IEnumerable<ContactType>> GetContactTypes() =>
            await _contactTypeService.GetContactTypes();

        /// <summary>
        /// Add a new Contact Type to the Phonebook.
        /// </summary>
        /// <param name="contactType">Contact Type data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> PostContactType(ContactType contactType) =>
            Created(nameof(ContactType), await _contactTypeService.CreateContactType(contactType));

        /// <summary>
        /// Update an existing Contact Type.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <param name="contactType">Contact Type data</param>
        /// <returns></returns>
        [HttpPut("{contactTypeId:int}")]
        public async Task<bool> PutContactType(int contactTypeId, ContactType contactType) =>
            await _contactTypeService.UpdatContactType(contactTypeId, contactType);

        /// <summary>
        /// Delete Contact Type by its identification.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactTypeId:int}")]
        public async Task<bool> DeleteContactType(int contactTypeId) =>
            await _contactTypeService.DeleteContactType(contactTypeId);
    }
}