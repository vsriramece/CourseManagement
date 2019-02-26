using Chama.CourseManagement.Domain.Entities;
using Chama.CourseManagement.Tests.Utilities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Chama.CourseManagement.Tests.Domain.Entities
{
    public class CourseTests
    {
        [Theory,AutoMoqData]
        public void Course_Signup_TeacherStudentSame_ThrowException(User student, Course sut)
        {
            // Arrange
            student.UserId = sut.TeacherUserId;
            // Act
            Action act = () => sut.Signup(student);
            // Assert
            act.Should().Throw<Exception>($"because student and teacher id are same").And.Message.Contains("Teacher could not be enrolled as a student");
        }

        [Theory, AutoMoqData]
        public void Course_Signup_ReachedMaxCapacity_ThrowException(List<UserCourse> students, User student, Course sut)
        {
            // Arrange
            sut.UserCourses = students;
            sut.TotalCapacity = students.Count;
            // Act
            Action act = () => sut.Signup(student);
            // Assert
            act.Should().Throw<Exception>($"because maximum capacity is reached").And.Message.Contains("reached the maximum enrollment limit.");
        }
    }
}
