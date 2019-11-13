﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Services.ContactType
{
    public interface IContactTypeService
    {
        Task<IEnumerable<Models.ContactType>> GetContactTypes();

        ValueTask<Models.ContactType> GetContactTypeById(int contactTypeId);

        Task<bool> CreateContactType(Models.ContactType contactType);

        Task<bool> UpdatContactType(int contactTypeId, Models.ContactType contactType);

        Task<bool> DeleteContactType(int contactTypeId);
    }
}
