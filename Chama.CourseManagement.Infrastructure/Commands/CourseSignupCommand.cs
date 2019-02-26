using Chama.CourseManagement.Infrastructure.DTO.Request;
using System;

namespace Chama.CourseManagement.Infrastructure.Commands
{
    public class CourseSignupCommand
    {
        public CourseSignupCommand(Guid courseId, SignupCourseRequest input)
        {
            CourseId = courseId;
            InputData = input;
        }
        public Guid CourseId { get; set; }
        public SignupCourseRequest InputData { get; set; }
    }
}
