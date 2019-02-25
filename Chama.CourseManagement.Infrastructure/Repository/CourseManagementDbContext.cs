using Chama.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public class CourseManagementDbContext: DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public CourseManagementDbContext(DbContextOptions<CourseManagementDbContext> options): base(options)
        {
        }

        protected CourseManagementDbContext(DbContextOptions options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });
            modelBuilder.Entity<UserCourse>()
                .HasOne(u => u.User)
                .WithMany(uc => uc.UserCourses)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserCourse>()
               .HasOne(c => c.Course)
               .WithMany(uc => uc.UserCourses)
               .HasForeignKey(uc => uc.CourseId);
        }
    }
}
