using Microsoft.EntityFrameworkCore;
using System;

namespace Surveys.Data.Definitions
{
    public interface IUnitOfWork : IDisposable
    {
        void Submit();
        void Rollback();
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
    }
}
