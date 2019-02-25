using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.CourseManagement.Infrastructure.DTO.Response.Reports
{
    public class CourseData: CourseReportBaseData
    {
        public string Teacher { get; set; }

        public IEnumerable<string> StudentList { get; set; }
    }
}
