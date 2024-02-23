using Microsoft.AspNetCore.Mvc;
using NewStarterProject.Model;
using NewStarterProject.NewStarter.Domain.Dtos;
using NewStarterProject.NewStarter.Domain.Interfaces;


namespace NewStarterProject.NewStarter.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            return Ok(await _userService.CreateUser(userDto));
        }

        // PUT api/<UserController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDto userDto)
        {
            return Ok(await _userService.UpdateUser(userDto));
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
            var userRecord = await _userService.GetUser(id);

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
