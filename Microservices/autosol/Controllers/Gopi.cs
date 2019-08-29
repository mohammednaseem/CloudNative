using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace autosol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GopiController : ControllerBase
    {
        // GET api/Gopi
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "gopis1", "gopis2" };
        }

        // GET api/gopis/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/gopis
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/gopis/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/gopis/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
