using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.CourseManagement.Infrastructure.DTO.Response.Reports
{
    public class CourseReportListData
    {
        public IEnumerable<CourseReportBaseData> CoursesReport { get; set; }
        public int TotalCoursesCount { get; set; }
    }
}
