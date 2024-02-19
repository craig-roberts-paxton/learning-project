using Microsoft.AspNetCore.Mvc;
using NewStarterProject.Dtos;
using NewStarterProject.Model;
using NewStarterProject.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewStarterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorController : ControllerBase
    {

        private DoorService _service;

        public DoorController(StarterProjectContext context)
        {
            _service = new DoorService(context);
        }

        
        // POST api/<DoorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DoorDto doorDto)
        {
            return Ok(await _service.CreateOrUpdateDoor(doorDto));
        }

        // PUT api/<DoorController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] DoorDto doorDto)
        {
            return Ok(await _service.CreateOrUpdateDoor(doorDto));
        }
        

        // GET: api/<DoorController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _service.GetAllDoors());
        }


        // GET api/<DoorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _service.Get(id));
        }

        
        // DELETE api/<DoorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteDoor(id);

            if (success)
            {
                return Ok();
            }

            return NotFound();

        }
    }
}
