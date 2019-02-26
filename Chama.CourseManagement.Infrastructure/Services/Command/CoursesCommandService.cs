using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chama.CourseManagement.Domain.Entities;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.DTO.Response;
using Chama.CourseManagement.Infrastructure.Repository;
using Chama.CourseManagement.Infrastructure.UnitOfWork;

namespace Chama.CourseManagement.Infrastructure.Services.Command
{
    public class CoursesCommandService : ICoursesCommandService
    {
        private ICourseRepository CourseRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        public CoursesCommandService(ICourseRepository repository, IUnitOfWork unitOfWork)
        {
            CourseRepository = repository;
            UnitOfWork = unitOfWork;
        }
        public Task<Guid> CreateCourse()
        {
            throw new NotImplementedException();
        }

        public async Task<SignupCourseResponse> SignupCourse(CourseSignupCommand command)
        {
            SignupCourseResponse result = new SignupCourseResponse();
            Course course = await CourseRepository.GetCourse(command.CourseId);
            if(course == null)
            {
                throw new Exception($"Course not found. CourseId: {command.CourseId}");
            }
            User user = await CourseRepository.GetUser(command.InputData.StudentId);
            if (user == null)
            {
                throw new Exception($"User not found. UserId: {command.InputData.StudentId}");
            }
            course.Signup(user);
            await UnitOfWork.Commit();
            result.Success = true;
            return result;
        }

    }
}
