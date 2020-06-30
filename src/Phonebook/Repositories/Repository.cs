using Microsoft.EntityFrameworkCore;
using Phonebook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Repositories
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : BaseEntity where TContext : DbContext
    { 
        protected readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public ValueTask<TEntity> GetById(int id) =>
            _context.Set<TEntity>().FindAsync(id);

        public IAsyncEnumerable<TEntity> GetAllReadOnlyAsEnumerable() =>
            _context.Set<TEntity>().AsNoTracking().AsAsyncEnumerable();

        public Task<List<TEntity>> GetAllReadOnly() =>
            _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).Property(c => c.Deleted).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Edit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
