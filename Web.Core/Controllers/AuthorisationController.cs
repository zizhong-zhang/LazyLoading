using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorisationController : ControllerBase
    {
        private readonly IAuthorisation _authorisation;

        public AuthorisationController(IAuthorisation authorisation)
        {
            _authorisation = authorisation;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _authorisation.Assert();
            return Ok();
        }
    }
}