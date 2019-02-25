using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.CourseManagement.Infrastructure.DTO.Response.Reports
{
    public class CourseReportBaseData
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int MinimumAge { get; set; }
        public int AverageAge { get; set; }
        public int MaximumAge { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentNumberOfStudents { get; set; }
    }
}
