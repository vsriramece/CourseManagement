using Chama.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public class CourseManagementDbContext: DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }

        private string DbConnection { get; }
        public CourseManagementDbContext(string dbConnection)
        {
            DbConnection = dbConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConnection);
        }
    }
}
