using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chama.CourseManagement.Infrastructure.DTO.Response.Reports;

namespace Chama.CourseManagement.Infrastructure.Services.Query
{
    public interface IReportsQueryService
    {
        Task<CourseReportListData> GetCoursesReport(int offset, int limit);
        Task<CourseReportData> GetCourseReport(Guid courseId);
    }
}
