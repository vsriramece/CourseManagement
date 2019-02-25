using System;

namespace Chama.CourseManagement.Domain.Entities
{
    public class UserCourse
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Course Course { get; set; }
    }
}
