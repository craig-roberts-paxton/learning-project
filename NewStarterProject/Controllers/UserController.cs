using Microsoft.AspNetCore.Mvc;
using NewStarterProject.Dtos;
using NewStarterProject.Model;
using NewStarterProject.Services;

namespace NewStarterProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(StarterProjectContext context)
        {
            _userService = new UserService(context);
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            return Ok(await _userService.CreateOrUpdateUser(userDto));
        }

        // PUT api/<UserController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDto userDto)
        {
            return Ok(await _userService.CreateOrUpdateUser(userDto));
        }


        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var userRecord = await _userService.Get(id);

            if (userRecord.UserId == -1)
                return BadRequest();

            return Ok(userRecord);

        }



        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUser(id);

            if (success)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
