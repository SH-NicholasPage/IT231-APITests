using APITests.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APITests.Server.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String Get()
        {
            return "Hello, World!";
        }
    }
}