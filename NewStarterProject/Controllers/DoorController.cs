using Microsoft.AspNetCore.Mvc;
using NewStarterProject.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewStarterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorController : ControllerBase
    {

        private StarterProjectContext _context;

        public DoorController(StarterProjectContext context)
        {
            _context = context;
        }

        // GET: api/<DoorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DoorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
