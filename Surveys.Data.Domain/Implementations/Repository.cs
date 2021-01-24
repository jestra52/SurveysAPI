using Microsoft.EntityFrameworkCore;
using Surveys.Data.Domain.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Implementations
{
    public class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TEntity item)
        {
            await GetSet().AddAsync(item);
            _unitOfWork.Submit();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetSet().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TEntityId id)
        {
            return !id.Equals(default(TEntityId)) ? await GetSet().FindAsync(id) : null;
        }

        public async Task RemoveAsync(TEntity item)
        {
            await Task.Run(() => GetSet().Remove(item));
            _unitOfWork.Submit();
        }

        public async Task<int> ExecuteSqlRawAsync(string query, params object[] parameters)
        {
            return await _unitOfWork.ExecuteSqlRawAsync(query, parameters);
        }

        protected DbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }
    }
}
