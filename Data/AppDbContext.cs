using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using quizmoon.Models;

namespace quizmoon.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasPostgresExtension("uuid-ossp");

            builder.Entity<Quiz>()
                .HasMany(q => q.QuizQuestions)
                .WithOne(qq => qq.Quiz)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizQuestion>()
                .HasMany(qq => qq.Answers)
                .WithOne(qa => qa.QuizQuestion)
                .HasForeignKey(qa => qa.QuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
    }
}