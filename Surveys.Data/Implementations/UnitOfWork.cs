using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Surveys.Data.Implementations
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options)
        {}

        #region DbSets

        public DbSet<Survey> Surveys { get; set; }

        #endregion

        public void Submit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}
