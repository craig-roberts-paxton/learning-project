using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NewStarterProject.Dtos;
using NewStarterProject.Model;
using NewStarterProject.Services;

namespace NewStarterProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessControlController : ControllerBase
    {

        private readonly AccessControlService _accessControlService;

        public AccessControlController(StarterProjectContext context)
        {
            _accessControlService = new AccessControlService(context);
        }


        // POST: api/<AccessControlController>/ValidateAccess/5
        [HttpPost("{id}")]
        public async Task<ActionResult> Details(int id, [FromBody] UserDto userDto)
        {
            var result = await _accessControlService.ValidateAccess(id, userDto);

            if (result)
                return Accepted();

            return Unauthorized();
        }


        // GET: api/<AccessControlController>/AllowAccessToDoor/5/1
        [HttpGet("/AllowAccessToDoor/{doorId}/{userId}")]
        public async Task<ActionResult> Create(int doorId, int userId)
        {
            var accessControlRecord = await _accessControlService.AllowAccessToDoor(doorId, userId);

            if (accessControlRecord.AccessControlDoorsToUsersId > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE: api/<AccessControlController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAccessFromDoor(int id)
        {
            var result = await _accessControlService.RemoveAccessFromDoor(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }


        // GET: api/<AccessControlController>/GetUsersAssignedToDoor/5
        [HttpGet("/GetUsersAssignedToDoor/{doorId}")]
        public async Task<ActionResult> GetUsersAssignedToDoor(int doorId)
        {
            return Ok(await _accessControlService.GetUsersAssignedToDoor(doorId));
        }


        // GET: api/<AccessControlController>/GetDoorsAssignedToUser/5
        [HttpGet("/GetDoorsAssignedToUser/{userId}")]
        public async Task<ActionResult> GetDoorsAssignedToUser(int userId)
        {
            return Ok(await _accessControlService.GetDoorsAssignedToUser(userId));
        }


        // GET api/<AccessControlController>/Audits/5/1
        [HttpGet("/Audits/{doorId}/{userId}")]
        public async Task<ActionResult> GetAudits(int? doorId, int? userId)
        {
            return Ok(await _accessControlService.GetAccessRecords(doorId, userId));
        }

    }
}
