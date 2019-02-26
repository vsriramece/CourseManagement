using Chama.Common.Email;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chama.CourseManagement.Webjob.MessageProcessor
{
    public class CourseSignupProcessor
    {
        private readonly ICoursesCommandService CommandService;
        private readonly IEmailProvider EmailProvider;
        public CourseSignupProcessor(ICoursesCommandService commandService, IEmailProvider emailProvider)
        {
            CommandService = commandService;
            EmailProvider = emailProvider;
        }

        public async Task ProcessQueueMessage([ServiceBusTrigger("coursesignup", Connection = "ServiceBus")] string message, ILogger logger)
        {
            try
            {
                var command = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseSignupCommand>(message);
                await CommandService.SignupCourse(command);
                //Email success - Mock data
                await EmailProvider.SendEmail("user@abc.com", "Signup success!", "Success");
            }
            catch(Exception ex)
            {
                //Log exception and Email failure
                await EmailProvider.SendEmail("user@abc.com", "Signup failed!", "Failure");
            }

        }
    }

}
