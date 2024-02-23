using Microsoft.AspNetCore.Mvc;
using NewStarter.Application.Interfaces;
using NewStarter.Domain.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewStarter.Api.New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorController : ControllerBase
    {

        private readonly IDoorService _doorService;

        public DoorController(IDoorService doorService)
        {
            _doorService = doorService;
        }


        // POST api/<DoorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DoorDto doorDto)
        {
            return Ok(await _doorService.CreateDoor(doorDto));
        }

        // PUT api/<DoorController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] DoorDto doorDto)
        {
            return Ok(await _doorService.UpdateDoor(doorDto));
        }


        // GET: api/<DoorController>
        [HttpGet]
        public async Task<List<DoorDto>> Get()
        {
            return await _doorService.GetActiveDoorList();
        }


        // GET api/<DoorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _doorService.GetDoor(id));
        }


        // DELETE api/<DoorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _doorService.DeleteDoor(id);

            if (success)
            {
                return Ok();
            }

            return NotFound();

        }
    }
}
