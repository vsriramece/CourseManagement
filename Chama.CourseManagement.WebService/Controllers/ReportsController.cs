using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.CourseManagement.Infrastructure.Services.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chama.CourseManagement.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsQueryService QueryService;
        public ReportsController(IReportsQueryService reportsQueryService)
        {
            QueryService = reportsQueryService;
        }

        [HttpGet("/Courses")]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}