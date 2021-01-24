using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Definitions
{
    public interface IUnitOfWork : IDisposable
    {
        void Submit();
        void Rollback();
        Task<int> ExecuteSqlRawAsync(string query, params object[] parameters);
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
    }
}
