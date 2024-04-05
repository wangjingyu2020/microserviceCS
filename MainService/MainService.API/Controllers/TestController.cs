using Microsoft.AspNetCore.Mvc;

namespace MainService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController
    {
        [HttpGet]
        public Task<String>? getTest()
        {
            return Task.FromResult("main test");
        }
    }
}
