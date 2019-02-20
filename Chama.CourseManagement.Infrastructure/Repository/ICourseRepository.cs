using Chama.CourseManagement.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public interface ICourseRepository
    {
        Task<Course> GetCourse(Guid courseId);
    }
}
