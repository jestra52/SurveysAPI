using Microsoft.EntityFrameworkCore;
using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Implementations
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options)
        {}

        #region DbSets

        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionOrder> QuestionOrder { get; set; }
        public virtual DbSet<Respondent> Respondent { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<SurveyResponse> SurveyResponse { get; set; }
        public virtual DbSet<VSurveyResponses> VSurveyResponses { get; set; }

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

        public void Edit<TEntity>(TEntity item) where TEntity : class
        {
            Update(item);
        }

        public void EditMany<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            UpdateRange(items);
        }

        public async Task<int> ExecuteSqlRawAsync(string query, params object[] parameters)
        {
            return await Database.ExecuteSqlRawAsync(query, parameters);
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Text)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<QuestionOrder>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.SurveyId })
                    .HasName("PK__Question__C794EE5BE86718F4");

                entity.ToTable("QuestionOrder");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOrders)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__QuestionO__Quest__2EDAF651");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.QuestionOrders)
                    .HasForeignKey(d => d.SurveyId)
                    .HasConstraintName("FK__QuestionO__Surve__2FCF1A8A");
            });

            modelBuilder.Entity<Respondent>(entity =>
            {
                entity.ToTable("Respondent");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(e => new { e.SurveyResponseId, e.QuestionId, e.RespondentId })
                    .HasName("PK__Response__2C5499A246776A05");

                entity.ToTable("Response");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Responses)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Response__Questi__339FAB6E");

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.Responses)
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Response__Respon__3493CFA7");

                entity.HasOne(d => d.SurveyResponse)
                    .WithMany(p => p.Responses)
                    .HasForeignKey(d => d.SurveyResponseId)
                    .HasConstraintName("FK__Response__Survey__32AB8735");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SurveyResponse>(entity =>
            {
                entity.ToTable("SurveyResponse");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.RespondentId)
                    .HasConstraintName("FK__SurveyRes__Respo__18EBB532");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.SurveyId)
                    .HasConstraintName("FK__SurveyRes__Surve__17F790F9");
            });

            modelBuilder.Entity<VSurveyResponses>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VSurveyResponses");

                entity.Property(v => v.SurveyId).HasColumnName("SurveyId");
                entity.Property(v => v.SurveyName).HasColumnName("SurveyName");
                entity.Property(v => v.SurveyDescription).HasColumnName("SurveyDescription");
                entity.Property(v => v.TotalResponses).HasColumnName("TotalResponses");
            });
        }
    }
}
