using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoardGamesManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesManagement.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BoardGamesContext _dbContext;
        public Repository(BoardGamesContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddItem(TEntity item)
        {
            await _dbContext.Set<TEntity>().AddAsync(item);

            return item;
        }

        public async Task<TEntity> GetItem(Guid id)
            => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<IEnumerable<TEntity>> GetItems(int? limit)
        {
            return limit.HasValue ? await _dbContext.Set<TEntity>().AsQueryable().Take(limit.Value).ToListAsync() : 
                        await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetItems(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AsQueryable().Where(expression).ToListAsync();
        }

        public TEntity UpdateItem(TEntity item)
        {
            var updatedItem = _dbContext.Update(item);
            return updatedItem.Entity;
        }

        public async Task DeleteItem(Guid id)
        {
            var item = await _dbContext.Set<TEntity>().FirstAsync(p => p.Id.Equals(id));
            _dbContext.Set<TEntity>().Remove(item);
        }

        public async Task DeleteItem(Expression<Func<TEntity, bool>> expression)
        {
            var item = await _dbContext.Set<TEntity>().FirstAsync(expression);
            _dbContext.Set<TEntity>().Remove(item);
        }
    }
}
