using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.CourseManagement.Infrastructure.DTO.Response.Reports;
using Chama.CourseManagement.Infrastructure.Services.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chama.CourseManagement.WebService.Controllers
{
    [Route("api/reports/courses")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsQueryService QueryService;
        public ReportsController(IReportsQueryService reportsQueryService)
        {
            QueryService = reportsQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesReport([FromQuery]int offset=1, [FromQuery]int limit =1)
        {
            try
            {
                var result = await QueryService.GetCoursesReport(offset, limit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseReport(Guid courseId)
        {
            try
            {
                var result = await QueryService.GetCourseReport(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}