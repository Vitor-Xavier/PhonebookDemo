using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;
using Phonebook.Services.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Find a Contact by its identification.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <returns>Contact</returns>
        [HttpGet("{contactId:int}")]
        public async Task<Contact> FindContactById(int contactId) =>
            await _contactService.GetContactById(contactId);

        /// <summary>
        /// Returns Contacts.
        /// </summary>
        /// <returns>List of Contacts</returns>
        [HttpGet("Person/{personId:int}")]
        public async Task<IEnumerable<Contact>> GetContactsByPerson(int personId) =>
            await _contactService.GetContactsByPerson(personId);

        /// <summary>
        /// Add a new Contact to the Phonebook.
        /// </summary>
        /// <param name="contact">Contact data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> PostContact(Contact contact) =>
            Created(nameof(Contact), await _contactService.CreateContact(contact));

        /// <summary>
        /// Update an existing Contact.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <param name="contact">Contact data</param>
        /// <returns></returns>
        [HttpPut("{contactId:int}")]
        public async Task<bool> PutContact(int contactId, Contact contact) =>
            await _contactService.UpdateContact(contact);

        /// <summary>
        /// Delete Contact by its identification.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactId:int}")]
        public async Task<bool> DeleteContact(int contactId) =>
            await _contactService.DeleteContact(contactId);
    }
}