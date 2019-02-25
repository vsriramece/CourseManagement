
using System;
using System.Collections.Generic;

namespace Chama.CourseManagement.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}
