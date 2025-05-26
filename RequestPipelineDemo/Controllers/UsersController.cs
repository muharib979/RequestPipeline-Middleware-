using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestPipelineDemo.Filters;
using System.ComponentModel.DataAnnotations;

namespace RequestPipelineDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public class UserDto
        {
            public string? Name { get; set; }

            [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
            public int Age { get; set; }
        }

        [HttpPost]
        [ValidateModel] // Apply validation filter
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            Response.Headers.Add("X-Powered-By", "UsersController");
            Response.Headers.Add("X-Request-Time", DateTime.UtcNow.ToString("o"));

            return Ok(new
            {
                message = "User created",
                user
            });
        }
    }
}
