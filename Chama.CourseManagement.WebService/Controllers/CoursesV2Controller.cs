using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.Common.Messaging;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chama.CourseManagement.WebService.Controllers
{
    [Route("courses/v2]")]
    [ApiController]
    public class CoursesV2Controller : ControllerBase
    {
        private readonly IMessagingClient MessagingClient;
        public CoursesV2Controller(IMessagingClient messagingClient)
        {
            MessagingClient = messagingClient;
        }
        [HttpPut("/{courseId}/signup")]
        public async Task<IActionResult> SignupCourseV2(Guid courseId, [FromBody]SignupCourseRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var result = await MessagingClient.PublishMessage(new CourseSignupCommand(courseId, request));
                return Accepted();
            }
            catch (Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}