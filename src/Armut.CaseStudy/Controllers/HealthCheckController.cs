using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Armut.CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HealthCheckController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Application running";
        }

        [Authorize]
        [HttpGet("auth")]
        public string GetAuth()
        {
            return "Application authorization running";
        }
    }
}
