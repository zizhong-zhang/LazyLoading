using Common;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuidGeneratorController : ControllerBase
    {
        private readonly IGuidGenerator _guidGenerator;

        public GuidGeneratorController(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_guidGenerator.GetGuid());
        }
    }
}
