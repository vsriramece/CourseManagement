using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.Repository;
using Chama.CourseManagement.Infrastructure.Services;
using Chama.CourseManagement.Infrastructure.Services.Command;
using Chama.CourseManagement.Infrastructure.UnitOfWork;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.CourseManagement.Webjob.MessageProcessor
{
    public class CourseSignupProcessor
    {
        //private ICoursesCommandService CommandService;

        // TO do - Dependency injection
        //public CourseSignupProcessor(ICoursesCommandService commandService)
        //{
        //   // CommandService = commandService;
        //}

        public static void ProcessQueueMessage([ServiceBusTrigger("coursesignup", Connection = "ServiceBus")] string message, ILogger logger)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseManagementDbContext>();
            optionsBuilder.UseSqlServer(Program.Configuration.GetConnectionString("CourseManagementDBConnection"));
            CourseManagementDbContext dbContext = new CourseManagementDbContext(optionsBuilder.Options);
            ICourseRepository repository = new CourseRepository(dbContext);
            IUnitOfWork uow = new UnitOfWork(dbContext);
            ICoursesCommandService CommandService = new CoursesCommandService(repository, uow);
            try
            {
                var command = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseSignupCommand>(message);
                CommandService.SignupCourse(command);
                //Email success
            }
            catch(Exception ex)
            {
                //Email failure
            }

        }
    }

}
