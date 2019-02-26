using AutoFixture.Xunit2;
using Chama.CourseManagement.Domain.Entities;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.DTO.Response;
using Chama.CourseManagement.Infrastructure.Repository;
using Chama.CourseManagement.Infrastructure.Services.Command;
using Chama.CourseManagement.Tests.Utilities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Chama.CourseManagement.Tests.Infrastructure
{
    public class CoursesCommandServiceTests
    {
        [Theory, AutoMoqData]
        public void SignupCourse_ThrowsException_When_Course_IsNull([Frozen]Mock<ICourseRepository> repository,
           CourseSignupCommand command,
            CoursesCommandService sut)
        {
            // Arrange
            repository.Setup(o => o.GetCourse(command.CourseId)).Returns<Course>(null);
            // Act
            Action act = ()=>sut.SignupCourse(command).GetAwaiter().GetResult();
            // Assert
            act.Should().Throw<Exception>($"because course is null").And.Message.Contains("Course not found");
        }

        [Theory, AutoMoqData]
        public async void SignupCourse_Success([Frozen]Mock<ICourseRepository> repository,
           CourseSignupCommand command,
           Course course,
           User user,
           CoursesCommandService sut)
        {
            // Arrange
            repository.Setup(o => o.GetCourse(command.CourseId)).ReturnsAsync(course);
            repository.Setup(o => o.GetUser(command.InputData.StudentId)).ReturnsAsync(user);
            int preSignUpUserCount = course.UserCourses.Count;
            // Act
            SignupCourseResponse result =await sut.SignupCourse(command);
            // Assert
            int postSignUpUserCount = course.UserCourses.Count;
            postSignUpUserCount.Should().Be(preSignUpUserCount + 1, "because user is added to the course");
            result.Success.Should().BeTrue("because result is success");
        }
    }
}
