using Microsoft.AspNetCore.Mvc;
using NewStarterProject.NewStarter.Domain.Dtos;
using NewStarterProject.NewStarter.Domain.Interfaces;

namespace NewStarterProject.NewStarter.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessControlController : ControllerBase
    {

        private readonly IAccessControlService _accessControlService;

        public AccessControlController(IAccessControlService accessControlService)
        {
            _accessControlService = accessControlService;
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
        [HttpGet("AllowAccessToDoor/{doorId}/{userId}")]
        public async Task<ActionResult> Create(int doorId, int userId)
        {
            var accessControlRecord = await _accessControlService.AllowAccessToDoor(doorId, userId);

            if (accessControlRecord.AccessControlDoorsToUsersId > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE: api/<AccessControlController>/5/1
        [HttpDelete("{doorid}/{userId}")]
        public async Task<ActionResult> RemoveAccessFromDoor(int doorId, int userId)
        {
            var result = await _accessControlService.RemoveAccessFromDoor(doorId, userId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }


        // GET: api/<AccessControlController>/GetUsersAssignedToDoor/5
        [HttpGet("doors/{doorId}/users")]
        public async Task<ActionResult> GetUsersAssignedToDoor(int doorId)
        {
            return Ok(await _accessControlService.GetUsersAssignedToDoor(doorId));
        }

        // GET: api/<AccessControlController>/GetUsersAssignedToDoor/5
        [HttpGet("doors/{doorId}/assignableusers")]
        public async Task<ActionResult> GetUsersNotAssignedToDoor(int doorId)
        {
            return Ok(await _accessControlService.GetUsersNotAssignedToDoor(doorId));
        }




        // GET: api/<AccessControlController>/GetDoorsAssignedToUser/5
        [HttpGet("users/{userId}/doors")]
        public async Task<ActionResult> GetDoorsAssignedToUser(int userId)
        {
            return Ok(await _accessControlService.GetDoorsAssignedToUser(userId));
        }


        // GET api/<AccessControlController>/Audits/5/1
        [HttpGet("Audits/{doorId}/{userId}")]
        public async Task<ActionResult> GetAudits(int doorId, int userId)
        {
            return Ok(await _accessControlService.GetAccessRecords(doorId, userId));
        }

    }
}
