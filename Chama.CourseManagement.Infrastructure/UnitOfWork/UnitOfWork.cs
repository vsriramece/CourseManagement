using Chama.CourseManagement.Infrastructure.Repository;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CourseManagementDbContext Dbcontext;
        public UnitOfWork(CourseManagementDbContext dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public async Task Commit()
        {
            await Dbcontext.SaveChangesAsync();
        }
    }
}
