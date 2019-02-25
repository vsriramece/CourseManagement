using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.CourseManagement.Infrastructure.DTO.Response.Reports
{
    public class CourseReportCollection
    {
        public IEnumerable<CourseReportBaseData> CoursesReport { get; set; }
    }
}
