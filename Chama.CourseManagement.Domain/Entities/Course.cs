using System;
using System.Collections.Generic;
using System.Linq;

namespace Chama.CourseManagement.Domain.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int TotalCapacity { get; set; }
        public Guid TeacherUserId { get; set; }
        // EF core doesn't support many-to many relationship elegantly until now. 
        // Development in progress. Until then, an explicit relationship entity is required
        public IList<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

        public Course()
        {
            
        }
        public void Signup(User student)
        {
            if(student.UserId == TeacherUserId)
            {
                throw new Exception($"Teacher could not be enrolled as a student. User Id:{student.UserId}");
            }
            if(UserCourses?.Count == TotalCapacity)
            {
                throw new Exception($"The course :{CourseName} reached the maximum enrollment limit.");
            }
            if (UserCourses?.FirstOrDefault(o=>o.UserId==student.UserId) != null)
            {
                throw new Exception($"Student already enrolled in the course. User Id :{student.UserId}");
            }
            UserCourses.Add(new UserCourse() { Course = this, CourseId= this.CourseId, User = student, UserId = student.UserId});
        }
    }
}
