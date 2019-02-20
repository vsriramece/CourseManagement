using System;
using System.Collections.Generic;

namespace Chama.CourseManagement.Domain.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int TotalCapacity { get; set; }
        public User Teacher { get; set; }
        public List<User> Students { get; set; }

        public void Signup(Guid studentId)
        {
            if(studentId == Teacher.UserId)
            {
                throw new Exception($"Teacher could not be enrolled as a student. Id:{studentId}");
            }
            if(Students?.Count == TotalCapacity)
            {
                throw new Exception($"The course :{CourseName} reached the maximum enrollment limit.");
            }
        }
    }
}
