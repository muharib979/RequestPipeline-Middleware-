using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RequestPipelineDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Middleware Working Successfully!");
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
            throw new Exception("এইটা ইচ্ছাকৃত exception!");
        }
    }
}
