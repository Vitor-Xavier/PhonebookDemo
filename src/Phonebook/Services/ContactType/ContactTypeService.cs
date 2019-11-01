using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Phonebook.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly ILogger _logger;

        private readonly PhonebookContext _context;

        private readonly IMemoryCache _memoryCache;

        public ContactTypeService(ILogger<ContactTypeService> logger, IMemoryCache memoryCache, PhonebookContext context)
        {
            _logger = logger;
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Models.ContactType>> GetContactTypes() =>
            await _memoryCache.GetOrCreateAsync("contacts", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                return _context.ContactTypes.AsNoTracking().ToListAsync();
            });

        public async Task<Models.ContactType> GetContactTypeById(int contactTypeId) =>
            await _context.ContactTypes.AsNoTracking().SingleOrDefaultAsync(c => c.ContactTypeId.Equals(contactTypeId));

        public async Task<bool> CreateContactType(Models.ContactType contactType)
        {
            if (!IsValid(contactType)) return false;

            _context.ContactTypes.Add(contactType);
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> UpdatContactType(int contactTypeId, Models.ContactType contactType)
        {
            if (!IsValid(contactType)) return false;
            contactType.ContactTypeId = contactTypeId;

            _context.ContactTypes.Attach(contactType);
            _context.Entry(contactType).State = EntityState.Modified;
            return (await _context.SaveChangesAsync() == 1);
        }

        public async Task<bool> DeleteContactType(int contactTypeId)
        {
            var contactType = new Models.ContactType { ContactTypeId = contactTypeId, Deleted = true };
            _context.ContactTypes.Attach(contactType);
            _context.Entry(contactType).Property(c => c.Deleted).IsModified = true;
            return await _context.SaveChangesAsync() == 1;
        }

        public bool IsValid(Models.ContactType contactType)
        {
            if (contactType is null) return false;
            if (string.IsNullOrWhiteSpace(contactType.Name)) return false;

            _logger.LogInformation("Contact Type {0} is valid.", contactType.Name);
            return true;
        }
    }
}
