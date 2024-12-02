using ExpenseSharing.Application.Features.GroupFeatures.Commands;
using ExpenseSharing.Application.Features.GroupFeatures.Queries;
using ExpenseSharing.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseSharing.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (InvalidParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }

        [HttpPost("{groupId}/add-user")]
        public async Task<IActionResult> AddUserToGroup([FromRoute] AddUserToGroupCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response)
                    return Ok(new { message = "User successfully added to group" });
                return BadRequest();
            }
            catch (InvalidParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }

        [HttpPost("{groupId}/remove-user")]
        public async Task<IActionResult> RemoveUserFromGroup([FromRoute] RemoveUserFromGroupCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response)
                    return Ok(new { message = "User successfully removed from group" });
                return BadRequest();
            }
            catch (InvalidParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups([FromQuery] GetAllGroupsQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(new { groups = response });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroup([FromRoute] GetGroupByIdQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(new { group = response });
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }
    }
}
