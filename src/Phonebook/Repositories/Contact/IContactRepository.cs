using System.Collections.Generic;

namespace Phonebook.Repositories.Contact
{
    public interface IContactRepository : IRepository<Models.Contact>
    {
        IAsyncEnumerable<Models.Contact> GetContactsByType(int contactTypeId);

        IAsyncEnumerable<Models.Contact> GetContactsByPerson(int personId);
    }
}
