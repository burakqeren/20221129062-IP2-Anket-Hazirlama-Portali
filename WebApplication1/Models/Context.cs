using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Context : IdentityDbContext<AppUser, AppRole, string>
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAndQuestionBridge> surveyAndQuestionBridges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Response>()
                .HasIndex(r => new { r.AppUserId, r.QuestionId })
                .IsUnique();

            modelBuilder.Entity<SurveyAndQuestionBridge>()
            .HasKey(sq => new { sq.SurveyId, sq.QuestionId });
        }

    }
}
