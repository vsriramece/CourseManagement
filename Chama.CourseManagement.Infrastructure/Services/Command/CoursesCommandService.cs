using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chama.CourseManagement.Domain.Entities;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.DTO.Response;
using Chama.CourseManagement.Infrastructure.Repository;

namespace Chama.CourseManagement.Infrastructure.Services.Command
{
    public class CoursesCommandService : ICoursesCommandService
    {
        private ICourseRepository CourseRepository { get; }
        public CoursesCommandService(ICourseRepository repository)
        {
            CourseRepository = repository;
        }
        public Task<Guid> CreateCourse()
        {
            throw new NotImplementedException();
        }

        public async Task<SignupCourseResponse> SignupCourse(CourseSignupCommand command)
        {
            SignupCourseResponse result = new SignupCourseResponse();
            Course course = await CourseRepository.GetCourse(command.CourseId);
            course.Signup(command.InputData.StudentId);
            return result;
        }

    }
}
