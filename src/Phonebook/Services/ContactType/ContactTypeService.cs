using Microsoft.Extensions.Caching.Memory;
using Phonebook.Common;
using Phonebook.Repositories.ContactType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IContactTypeRepository _contactTypeRepository;

        private readonly IMemoryCache _memoryCache;

        public ContactTypeService(IMemoryCache memoryCache, IContactTypeRepository contactTypeRepository)
        {
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
            Models.ContactType contactType = new() { ContactTypeId = contactTypeId, Deleted = true };

            await _contactTypeRepository.Delete(contactType);
        }

        public bool IsValid(Models.ContactType contactType) => contactType is { Name: { Length: > 0 } };
    }
}
