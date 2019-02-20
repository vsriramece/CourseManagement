using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.Services
{
    public interface ICoursesCommandService
    {
        Task<Guid> CreateCourse();
    }
}
