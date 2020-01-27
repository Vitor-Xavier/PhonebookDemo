using Phonebook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<TEntity> GetById(int id);

        Task<List<TEntity>> GetAllReadOnly();

        IAsyncEnumerable<TEntity> GetAllReadOnlyAsEnumerable();

        Task Add(TEntity entity);

        Task Delete(TEntity Entity);

        Task Edit(TEntity entity);
    }
}
