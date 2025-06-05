using Microsoft.EntityFrameworkCore;

namespace QuizApp.Entity
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerResult> AnswerResults { get; set; }
        public DbSet<AnswerResult_Question> AnswerResult_Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerResult_Question>()
                .HasKey(q => new { q.ResultId, q.QuestionId });
        }
    }
}