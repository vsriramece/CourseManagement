using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateCourseRequest request)
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
    }
}