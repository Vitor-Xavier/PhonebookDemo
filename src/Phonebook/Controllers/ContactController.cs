using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.Contact;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
namespace Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) =>
            _contactService = contactService;

        /// <summary>
        /// Find a Contact by its identification.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Contact</returns>
        [HttpGet("{contactId:int}")]
        public ValueTask<Contact> FindContactById(int contactId, CancellationToken cancellationToken) =>
            _contactService.GetContactById(contactId, cancellationToken);

        /// <summary>
        /// Returns Contacts.
        /// </summary>
        /// <param name="personId">Person identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>List of Contacts</returns>
        [HttpGet("Person/{personId:int}")]
        public IAsyncEnumerable<Contact> GetContactsByPerson(int personId, CancellationToken cancellationToken) =>
            _contactService.GetContactsByPerson(personId, cancellationToken);

        /// <summary>
        /// Add a new Contact to the Phonebook.
        /// </summary>
        /// <param name="contact">Contact data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Created contact</returns>
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact, CancellationToken cancellationToken)
        {
            await _contactService.CreateContact(contact, cancellationToken);
            return Created(nameof(Contact), contact);
        }

        /// <summary>
        /// Update an existing Contact.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <param name="contact">Contact data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Updated contact</returns>
        [HttpPut("{contactId:int}")]
        public async Task<ActionResult<Contact>> PutContact(int contactId, Contact contact, CancellationToken cancellationToken) 
        {
            await _contactService.UpdateContact(contactId, contact, cancellationToken);
            return Ok(contact);
        }

        /// <summary>
        /// Delete Contact by its identification.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns></returns>
        [HttpDelete("{contactId:int}")]
        public async Task<ActionResult> DeleteContact(int contactId, CancellationToken cancellationToken)
        {
            await _contactService.DeleteContact(contactId, cancellationToken);
            return NoContent();
        }
    }
}