using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Surveys.Data.Definitions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity item);

        Task RemoveAsync(TEntity item);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
