using Chama.CourseManagement.Infrastructure.BusinessObjects.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public interface IReportRespository
    {
        Task<CourseReportList> GetStatisticsForCourseList(int offset, int limit);
        Task<CourseReport> GetStatisticsForCourse(Guid courseId);
    }
}
