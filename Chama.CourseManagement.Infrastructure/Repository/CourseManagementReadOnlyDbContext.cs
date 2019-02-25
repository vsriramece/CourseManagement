using Microsoft.EntityFrameworkCore;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public class CourseManagementReadOnlyDbContext : CourseManagementDbContext
    {
        public CourseManagementReadOnlyDbContext(DbContextOptions<CourseManagementReadOnlyDbContext> options) : base(options)
        {
        }
    }
}
