using Chama.CourseManagement.Infrastructure.DTO.Response.Reports;
using Chama.CourseManagement.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.Services.Query
{
    public class ReportsQueryService: IReportsQueryService
    {
        private readonly IReportRespository Repository;
        public ReportsQueryService(IReportRespository reportRespository)
        {
            Repository = reportRespository;
        }

        public async Task<CourseReportListData> GetCoursesReport(int offset, int limit)
        {
            var courseList = await Repository.GetStatisticsForCourseList(offset, limit);

            // To do -Automapper can be used
            var result = new CourseReportListData()
            {
                CoursesReport = courseList?.CoursesReport.Select(o=>new CourseReportBaseData {
                    MinimumAge=o.MinimumAge,
                    MaximumAge=o.MaximumAge,
                    AverageAge =o.AverageAge,
                    CourseId =o.CourseId,
                    CourseName=o.CourseName,
                    TotalCapacity=o.TotalCapacity,
                    CurrentNumberOfStudents = o.CurrentNumberOfStudents
                }),
                TotalCoursesCount = courseList.TotalCourses
            };
            return result;
        }

        public async Task<CourseReportData> GetCourseReport(Guid courseId)
        {
            var courseReport = await Repository.GetStatisticsForCourse(courseId);
            CourseReportData result = null;
            if(courseReport!= null)
            {
                result = new CourseReportData()
                {
                    MinimumAge = courseReport.MinimumAge,
                    MaximumAge = courseReport.MaximumAge,
                    AverageAge = courseReport.AverageAge,
                    CourseId = courseReport.CourseId,
                    CourseName = courseReport.CourseName,
                    TotalCapacity = courseReport.TotalCapacity,
                    CurrentNumberOfStudents = courseReport.CurrentNumberOfStudents,
                    TeacherId = courseReport.TeacherId,
                    StudentList = courseReport.StudentList
                };
            }
            return result;
        }

    }
}
