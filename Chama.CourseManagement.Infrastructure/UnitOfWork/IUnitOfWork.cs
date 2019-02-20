using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
