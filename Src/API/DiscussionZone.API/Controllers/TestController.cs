using Microsoft.AspNetCore.Mvc;

namespace DiscussionZone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ILogger<TestController> logger) : ControllerBase
    {
        private readonly ILogger<TestController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            _logger.LogInformation("Loglama işlemi tamamlandı");
            return Ok();
        }
    }
}
