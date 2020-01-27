using System.Collections.Generic;

namespace Phonebook.Repositories.ContactType
{
    public interface IContactTypeRepository : IRepository<Models.ContactType>
    {
        IAsyncEnumerable<Models.ContactType> GetContactTypesByName(string name);
    }
}
