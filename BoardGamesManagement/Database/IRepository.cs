using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoardGamesManagement.Domain;

namespace BoardGamesManagement.Database
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        /// <summary>
        /// Adds item to database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Items saved in databse</returns>
        Task<TEntity> AddItem(TEntity item);

        /// <summary>
        /// Get item by given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Item of given ID</returns>
        Task<TEntity> GetItem(Guid id);

        /// <summary>
        /// Get list of items with given limit of items (if limit is null, then returns all items)
        /// </summary>
        /// <param name="limit">Limit of returned items - if null returns all</param>
        /// <returns>List of items</returns>
        Task<IEnumerable<TEntity>> GetItems(int? limit);

        /// <summary>
        /// Get list of items by provided expression
        /// </summary>
        /// <param name="expression">Expression that determines items to be selected</param>
        /// <returns>List of items</returns>
        Task<IEnumerable<TEntity>> GetItems(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Updates item in database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated item</returns>
        TEntity UpdateItem(TEntity item);

        /// <summary>
        /// Removes item from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteItem(Guid id);

        /// <summary>
        /// Removes item from database
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task DeleteItem(Expression<Func<TEntity, bool>> expression);
    }
}
