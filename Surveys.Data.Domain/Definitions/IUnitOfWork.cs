using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Definitions
{
    public interface IUnitOfWork : IDisposable
    {
        void Submit();
        void Rollback();
        void Edit<TEntity>(TEntity item) where TEntity : class;
        void EditMany<TEntity>(IEnumerable<TEntity> items) where TEntity : class;
        Task<int> ExecuteSqlRawAsync(string query, params object[] parameters);
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
    }
}
