using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Phonebook.Common;
using Phonebook.Repositories.ContactType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly ILogger _logger;

        private readonly IContactTypeRepository _contactTypeRepository;

        private readonly IMemoryCache _memoryCache;

        public ContactTypeService(ILogger<ContactTypeService> logger, IMemoryCache memoryCache, IContactTypeRepository contactTypeRepository)
        {
            _logger = logger;
            _contactTypeRepository = contactTypeRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Models.ContactType>> GetContactTypes() =>
            await _memoryCache.GetOrCreateAsync(CacheKeys.ContactType, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                return _contactTypeRepository.GetAllReadOnly();
            });

        public async ValueTask<Models.ContactType> GetContactTypeById(int contactTypeId) =>
            await _contactTypeRepository.GetById(contactTypeId);

        public async Task CreateContactType(Models.ContactType contactType)
        {
            if (!IsValid(contactType)) return;

            await _contactTypeRepository.Add(contactType);
        }

        public async Task UpdatContactType(int contactTypeId, Models.ContactType contactType)
        {
            if (!IsValid(contactType)) return;
            contactType.ContactTypeId = contactTypeId;

            await _contactTypeRepository.Edit(contactType);
        }

        public async Task DeleteContactType(int contactTypeId)
        {
            var contactType = new Models.ContactType { ContactTypeId = contactTypeId, Deleted = true };

            await _contactTypeRepository.Delete(contactType);
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
