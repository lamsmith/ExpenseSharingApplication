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

        [HttpGet("current")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "User is not authenticated." });
                }

                if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
                {
                    return BadRequest(new { message = "Invalid user ID format." });
                }

                var query = new GetUserByIdQuery { UserId = currentUserId };
                var response = await _mediator.Send(query);
                return Ok(new { user = response });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "An error occurred while retrieving user details." });
            }
        }

        [HttpGet("current/groups")]
        public async Task<IActionResult> GetUserGroups()
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "User is not authenticated." });
                }

                if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
                {
                    return BadRequest(new { message = "Invalid user ID format." });
                }

                var query = new GetGroupsForUserQuery { UserId = currentUserId };
                var response = await _mediator.Send(query);
                return Ok(new { groups = response });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "An error occurred while retrieving user's groups." });
            }
        }

        [HttpGet("current/settlement-history")]
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

        [HttpPost("wallet/topup")]
        public async Task<IActionResult> DepositToWallet([FromBody] DepositToWalletCommand command)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "User is not authenticated." });
                }
                if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
                {
                    return BadRequest(new { message = "Invalid user ID format." });
                }
                command.UserId = currentUserId;  // Set the UserId from the authenticated user
                var response = await _mediator.Send(command);
                return Ok(new { message = "Wallet successfully deposited", balance = response });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while Deposited wallet." });
            }
        }


        [HttpGet("wallet/balance")]
        public async Task<IActionResult> GetWalletBalance()
        {
            try
            {

                var userId = User.FindFirst("UserId")?.Value;


                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User is not authenticated." });

                var query = new GetWalletBalanceQuery { UserId = Guid.Parse(userId) };

                var response = await _mediator.Send(query);
                return Ok(new { balance = response });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching wallet balance." });
            }
        }

    }
}



