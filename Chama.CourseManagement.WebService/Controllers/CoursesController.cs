using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.CourseManagement.Infrastructure.Commands;
using Chama.CourseManagement.Infrastructure.DTO.Request;
using Chama.CourseManagement.Infrastructure.DTO.Response;
using Chama.CourseManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chama.CourseManagement.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesCommandService CommandService;
        public CoursesController(ICoursesCommandService commandService)
        {
            CommandService = commandService;
        }

        // POST api/courses
        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse([FromBody]CreateCourseRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var courseId = await CommandService.CreateCourse();
                return Ok(new CreateCourseResponse { CourseId = courseId });
            }
            catch (Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("courses/{courseId}/signup")]
        public async Task<IActionResult> SignupCourse(Guid courseId,[FromBody]SignupCourseRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var result = await CommandService.SignupCourse(new CourseSignupCommand(courseId,request));
                return Ok(new SignupCourseResponse { Success = true });
            }
            catch (Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}