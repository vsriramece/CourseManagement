using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.DTO.Response;

namespace Chama.CourseManagement.Infrastructure.Services
{
    public interface ICoursesCommandService
    {
        Task<Guid> CreateCourse();
        Task<SignupCourseResponse> SignupCourse(CourseSignupCommand courseSignupCommand);
    }
}
