using ExpenseSharing.Application.Features.SettlementFeatures.Queries;
using ExpenseSharing.Application.Features.Users.Commands;
using ExpenseSharing.Application.Features.Users.Queries;
using ExpenseSharing.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ExpenseSharing.Application.Features.Expenses.Queries;

namespace ExpenseSharing.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] GetUserByIdQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(new { user = response });
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }

        [HttpGet("{userId}/groups")]
        public async Task<IActionResult> GetUserGroups([FromRoute] GetGroupsForUserQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(new { groups = response });
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }

        [HttpGet("{userId}/settlement-history")]
        public async Task<IActionResult> GetUserGroups([FromRoute] GetSettlementHistoryForUserQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(new { settlement_history = response });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting user's account." });
            }
        }
    }
}
