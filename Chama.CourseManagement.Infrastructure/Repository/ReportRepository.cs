using Chama.CourseManagement.Infrastructure.BusinessObjects.Reports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.Repository
{
    public class ReportRepository : IReportRespository
    {
        private CourseManagementDbContext CourseReadonlyDbContext { get; }
        public ReportRepository(CourseManagementReadOnlyDbContext dbContext)
        {
            CourseReadonlyDbContext = dbContext;
        }

        public async Task<CourseReportList> GetStatisticsForCourseList(int offset, int limit)
        {
            var courseList = CourseReadonlyDbContext.Courses
                                            .OrderBy(c => c.CourseId)
                                            .Skip((offset - 1) * limit)
                                            .Take(limit)
                                            .GroupBy(c => new { c.CourseId, c.CourseName, c.TotalCapacity })
                                            .Select(c => new CourseReportBase
                                            {
                                                CourseId = c.Key.CourseId,
                                                CourseName = c.Key.CourseName,
                                                TotalCapacity = c.Key.TotalCapacity,
                                                MinimumAge = c.SelectMany(s => s.UserCourses).Min(o => o.User.Age),
                                                MaximumAge = c.SelectMany(s => s.UserCourses).Max(o => o.User.Age),
                                                AverageAge = c.SelectMany(s => s.UserCourses).Average(o => o.User.Age),
                                                CurrentNumberOfStudents = c.SelectMany(s => s.UserCourses).Count()
                                            }).ToListAsync();

            var totalCount = CourseReadonlyDbContext.Courses.CountAsync();

            await Task.WhenAll(courseList, totalCount);

            return new CourseReportList() { CoursesReport = await courseList, TotalCourses = await totalCount };
        }

        public async Task<CourseReport> GetStatisticsForCourse(Guid courseId)
        {
            var course = await CourseReadonlyDbContext.Courses.Include(o=>o.UserCourses).ThenInclude(u=>u.User)
                                            .FirstOrDefaultAsync(c => c.CourseId == courseId);
            CourseReport result = null;
            if (course != null)
            {
                int userCount = course.UserCourses?.Count() ?? 0;
                result = new CourseReport()
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    TotalCapacity = course.TotalCapacity,
                    CurrentNumberOfStudents = userCount,
                    MinimumAge = userCount == 0?0:course.UserCourses.Min(o => o.User.Age),
                    MaximumAge = userCount == 0 ? 0 : course.UserCourses.Max(o => o.User.Age),
                    AverageAge = userCount == 0 ? 0 : course.UserCourses.Average(o => o.User.Age),
                    TeacherId = course.TeacherUserId,
                    StudentList = course.UserCourses.Select(o => o.User.Name)
                };
            }
            return result;
        }
    }
}
