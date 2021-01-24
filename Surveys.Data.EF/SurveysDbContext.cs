using Microsoft.EntityFrameworkCore;
using Surveys.Data.EF.Domain;

namespace Surveys.Data.EF
{
    public partial class SurveysDbContext : DbContext
    {
        public SurveysDbContext()
        {
        }

        public SurveysDbContext(DbContextOptions<SurveysDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionOrder> QuestionOrders { get; set; }
        public virtual DbSet<Respondent> Respondents { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyResponse> SurveyResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id).ValueGeneratedNever();

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
                entity.HasNoKey();

                entity.ToTable("QuestionOrder");

                entity.HasOne(d => d.Question)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuestionO__Quest__619B8048");

                entity.HasOne(d => d.Survey)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuestionO__Surve__628FA481");
            });

            modelBuilder.Entity<Respondent>(entity =>
            {
                entity.ToTable("Respondent");

                entity.Property(e => e.Id).ValueGeneratedNever();

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
                entity.HasNoKey();

                entity.ToTable("Response");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Response__Questi__693CA210");

                entity.HasOne(d => d.Respondent)
                    .WithMany()
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Response__Respon__6A30C649");

                entity.HasOne(d => d.SurveyResponse)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Response__Survey__68487DD7");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Id).ValueGeneratedNever();

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

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SurveyRes__Respo__66603565");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SurveyRes__Surve__656C112C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
