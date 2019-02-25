using System;
using System.Collections.Generic;

namespace Chama.CourseManagement.Infrastructure.BusinessObjects.Reports
{
    public class CourseReport: CourseReportBase
    {
        public Guid TeacherId { get; set; }

        public IEnumerable<string> StudentList { get; set; }
    }
}
