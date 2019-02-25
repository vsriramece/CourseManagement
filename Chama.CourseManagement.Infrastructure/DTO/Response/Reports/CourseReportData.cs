using System;
using System.Collections.Generic;

namespace Chama.CourseManagement.Infrastructure.DTO.Response.Reports
{
    public class CourseReportData: CourseReportBaseData
    {
        public Guid TeacherId { get; set; }

        public IEnumerable<string> StudentList { get; set; }
    }
}
