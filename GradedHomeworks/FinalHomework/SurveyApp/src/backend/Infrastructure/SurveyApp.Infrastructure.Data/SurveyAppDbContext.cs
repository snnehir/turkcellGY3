using Microsoft.EntityFrameworkCore;
using SurveyApp.Entities;

namespace SurveyApp.Infrastructure.Data
{
    public class SurveyAppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public SurveyAppDbContext(DbContextOptions<SurveyAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Survey>().HasOne(s => s.SurveyOwner)
                                         .WithMany(u => u.Surveys)
                                         .HasForeignKey(s => s.SurveyOwnerId);
            modelBuilder.Entity<Question>().HasOne(q => q.Survey)
                                           .WithMany(s => s.Questions)
                                           .HasForeignKey(q => q.SurveyId);
            modelBuilder.Entity<Question>().HasOne(q => q.QuestionType)
                                           .WithMany(qo => qo.Questions)
                                           .HasForeignKey(q => q.QuestionTypeId);
            modelBuilder.Entity<QuestionOption>().HasOne(qo => qo.Question)
                                                 .WithMany(q => q.QuestionOptions)
                                                 .HasForeignKey(qo => qo.QuestionId);
            modelBuilder.Entity<Response>().HasOne(r => r.Survey)
                                           .WithMany(s => s.Responses)
                                           .HasForeignKey(r => r.SurveyId)
                                           .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Answer>().HasOne(a => a.Response)
                                         .WithMany(r => r.Answers)
                                         .HasForeignKey(a => a.ResponseId)
                                         .OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<Answer>().HasOne(a => a.Question)
                                         .WithMany(q => q.Answers)
                                         .HasForeignKey(a => a.QuestionId)
                                         .OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<AnswerOption>().HasOne(ao => ao.Answer)
                                               .WithMany(a => a.AnswerOptions)
                                               .HasForeignKey(ao => ao.AnswerId)
                                               .OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<AnswerOption>().HasOne(ao => ao.QuestionOption)
                                               .WithMany(qo => qo.AnswerOptions)
                                               .HasForeignKey(ao => ao.QuestionOptionId)
                                               .OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<Notification>().HasOne(n => n.User)
                                               .WithMany(u =>  u.Notifications)
                                               .HasForeignKey(n => n.UserId);

            modelBuilder.Entity<Response>().Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        }
    }
}
