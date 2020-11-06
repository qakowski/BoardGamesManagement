using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoardGamesManagement.Database;
using BoardGamesManagement.Domain;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Linq;

namespace BoardGamesManagement.Database.NHibernateDb
{
    public class NHibRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private NHibernate.ISession _session = null;
        private ITransaction _transaction = null;
        private IQueryable<TEntity> _items;

        public NHibRepository(NHibernate.ISession session)
        {
            _session = session;
            _items = _session.Query<TEntity>();
        }

        public async Task<TEntity> AddItem(TEntity item)
        {
            await _session.SaveAsync(item);
            return item;
        }

        public async Task DeleteItem(Guid id)
        {
            var item = await _session.GetAsync(typeof(TEntity), id);
            await _session.DeleteAsync(item);
        }

        public async Task DeleteItem(Expression<Func<TEntity, bool>> expression)
        {
            var item = await _session.Query<TEntity>().Where(expression).FirstOrDefaultAsync();
            await _session.DeleteAsync(item);
        }

        public async Task<TEntity> GetItem(Guid id)
        {
            return await _items.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetItems(int? limit)
        {
            if (limit.HasValue)
                return await _items.Take(limit.Value).ToListAsync();

            return await _items.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetItems(Expression<Func<TEntity, bool>> expression)
        {
            return await _items.Where(expression).ToListAsync();
        }

        public TEntity UpdateItem(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
