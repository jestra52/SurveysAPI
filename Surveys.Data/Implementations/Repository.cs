using Microsoft.EntityFrameworkCore;
using Surveys.Data.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Surveys.Data.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
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

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = GetSet();

            if (filter != null)
                query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(TEntity item)
        {
            await Task.Run(() => GetSet().Remove(item));
            _unitOfWork.Submit();
        }

        private DbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }
    }
}
