using Microsoft.Extensions.Caching.Memory;
using Phonebook.Common;
using Phonebook.Exceptions;
using Phonebook.Repositories.ContactType;
using System;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<IEnumerable<Models.ContactType>> GetContactTypes(CancellationToken cancellationToken = default) =>
            await _memoryCache.GetOrCreateAsync(CacheKeys.ContactType, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                return _contactTypeRepository.GetAllReadOnly(cancellationToken);
            });

        public async ValueTask<Models.ContactType> GetContactTypeById(int contactTypeId, CancellationToken cancellationToken = default) =>
            await _contactTypeRepository.GetById(contactTypeId, cancellationToken);

        public async Task CreateContactType(Models.ContactType contactType, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!IsValid(contactType)) throw new BadRequestException("Registro inválido");

            await _contactTypeRepository.Add(contactType, cancellationToken);
        }

        public async Task UpdatContactType(int contactTypeId, Models.ContactType contactType, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!IsValid(contactType)) throw new BadRequestException("Registro inválido");
            contactType.ContactTypeId = contactTypeId;

            await _contactTypeRepository.Edit(contactType, cancellationToken);
        }

        public async Task DeleteContactType(int contactTypeId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Models.ContactType contactType = new() { ContactTypeId = contactTypeId, Deleted = true };

            await _contactTypeRepository.Delete(contactType, cancellationToken);
        }

        public bool IsValid(Models.ContactType contactType) => contactType is { Name: { Length: > 0 } };
    }
}
