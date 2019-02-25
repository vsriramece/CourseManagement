using System;

namespace Chama.CourseManagement.Infrastructure.BusinessObjects.Reports
{
    public class CourseReportBase
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int MinimumAge { get; set; }
        public double AverageAge { get; set; }
        public int MaximumAge { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentNumberOfStudents { get; set; }
    }
}
