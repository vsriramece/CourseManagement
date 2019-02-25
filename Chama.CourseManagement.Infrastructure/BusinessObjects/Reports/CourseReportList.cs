using System.Collections.Generic;

namespace Chama.CourseManagement.Infrastructure.BusinessObjects.Reports
{
    public class CourseReportList
    {
        public IEnumerable<CourseReportBase> CoursesReport { get; set; }
        public int TotalCourses { get; set; }
    }
}
