﻿using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Extensions;
using Phonebook.Models;
using Phonebook.Services.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
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
        public ValueTask<Contact> FindContactById(int contactId) =>
            _contactService.GetContactById(contactId);

        /// <summary>
        /// Returns Contacts.
        /// </summary>
        /// <returns>List of Contacts</returns>
        [HttpGet("Person/{personId:int}")]
        public IAsyncEnumerable<Contact> GetContactsByPerson(int personId) =>
            _contactService.GetContactsByPerson(personId);

        /// <summary>
        /// Add a new Contact to the Phonebook.
        /// </summary>
        /// <param name="contact">Contact data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> PostContact(Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            await _contactService.CreateContact(contact);
            return Created(nameof(Contact), contact);
        }

        /// <summary>
        /// Update an existing Contact.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <param name="contact">Contact data</param>
        /// <returns></returns>
        [HttpPut("{contactId:int}")]
        public Task PutContact(int contactId, Contact contact) =>
            _contactService.UpdateContact(contactId, contact);

        /// <summary>
        /// Delete Contact by its identification.
        /// </summary>
        /// <param name="contactId">Contact identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactId:int}")]
        public Task DeleteContact(int contactId) =>
            _contactService.DeleteContact(contactId);
    }
}