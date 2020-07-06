using System.Threading.Tasks;
using Common;
using System.Web.Http;

namespace Web.Controllers
{
    public class AuthorisationController : ApiController
    {
        private readonly IAuthorisation _authorisation;
        public AuthorisationController(IAuthorisation authorisation)
        {
            _authorisation = authorisation;
        }

        // GET api/values
        public async Task<IHttpActionResult> Get()
        {
            await _authorisation.Assert();
            return Ok();
        }
    }
}
