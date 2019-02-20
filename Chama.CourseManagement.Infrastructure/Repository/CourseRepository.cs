using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chama.CourseManagement.Domain.Entities;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private CourseManagementDbContext CourseDbContext { get; }
        public CourseRepository(CourseManagementDbContext dbContext)
        {
            CourseDbContext = dbContext;
        }

        public async Task<Course> GetCourse(Guid courseId)
        {
            return await CourseDbContext.Courses.FindAsync(courseId);
        }
    }
}
