using CourseManagmentSystem.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseManagmentSystem.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext() : base("AppDbContext")
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<CourseThread> CourseThreads { get; set; }
        public DbSet<CourseMessage> CourseMessages { get; set; }

    }
}