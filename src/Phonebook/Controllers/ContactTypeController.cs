using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.ContactType;
using System.Collections.Generic;
using System.Threading;
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
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Person</returns>
        [HttpGet("{contactTypeId:int}")]
        public ValueTask<ContactType> FindContactTypeById(int contactTypeId, CancellationToken cancellationToken) =>
            _contactTypeService.GetContactTypeById(contactTypeId, cancellationToken);

        /// <summary>
        /// Returns Contact Types.
        /// </summary>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>List of Contact Types</returns>
        [HttpGet]
        public Task<IEnumerable<ContactType>> GetContactTypes(CancellationToken cancellationToken) =>
            _contactTypeService.GetContactTypes(cancellationToken);

        /// <summary>
        /// Add a new Contact Type to the Phonebook.
        /// </summary>
        /// <param name="contactType">Contact Type data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Created Contact Type</returns>
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactType contactType, CancellationToken cancellationToken)
        {
            await _contactTypeService.CreateContactType(contactType, cancellationToken);
            return Created(nameof(ContactType), contactType);
        }

        /// <summary>
        /// Update an existing Contact Type.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <param name="contactType">Contact Type data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Updated Contact Type</returns>
        [HttpPut("{contactTypeId:int}")]
        public async Task<ActionResult<ContactType>> PutContactType(int contactTypeId, ContactType contactType, CancellationToken cancellationToken)
        {
            await _contactTypeService.UpdatContactType(contactTypeId, contactType, cancellationToken);
            return Ok(contactType);
        }

        /// <summary>
        /// Delete Contact Type by its identification.
        /// </summary>
        /// <param name="contactTypeId">Contact Type identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns></returns>
        [HttpDelete("{contactTypeId:int}")]
        public async Task<ActionResult> DeleteContactType(int contactTypeId, CancellationToken cancellationToken)
        {
            await _contactTypeService.DeleteContactType(contactTypeId, cancellationToken);
            return NoContent();
        }
    }
}