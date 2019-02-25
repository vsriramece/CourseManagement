using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chama.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            return await CourseDbContext.Courses.Include(o=>o.UserCourses).FirstOrDefaultAsync(o=>o.CourseId == courseId);
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await CourseDbContext.Users.FindAsync(userId);
        }
    }
}
