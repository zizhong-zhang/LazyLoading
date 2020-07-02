using Common;
using System.Web.Http;

namespace Web.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IGuidGenerator _guidGenerator;
        public ValuesController(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        // GET api/values
        public IHttpActionResult Get()
        {
            return Ok(_guidGenerator.GetGuid());
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
