using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Definitions
{
    public interface IRepository<TEntity, in TEntityId> where TEntity : class
    {
        Task AddAsync(TEntity item);
        Task RemoveAsync(TEntity item);
        Task Edit(TEntity item);
        Task EditMany(IEnumerable<TEntity> items);
        Task<TEntity> GetAsync(TEntityId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> ExecuteSqlRawAsync(string query, params object[] parameters);
    }
}
