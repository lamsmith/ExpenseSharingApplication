using ExpenseSharing.Application.Features.ExpenseFeatures.Commands;
using ExpenseSharing.Application.Features.SettlementFeatures.Queries;
using ExpenseSharing.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ExpenseSharing.Application.Features.Expenses.Queries;
using ExpenseSharing.Application.Features.SettlementFeatures.Command;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseSharing.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(AddExpenseCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(new { id = response });
            }
            catch (InvalidParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedUserAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPost("{expenseId}/settlement-history")]
        public async Task<IActionResult> GetSettlementHistory([FromRoute] GetSettlementHistoryForExpenseQuery command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(new { settlement_history = response });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred." });
            }
        }


        [HttpPost("{expenseId}/pay-expense-share")]
        public async Task<IActionResult> PayExpenseShare([FromBody] CreateSettlementCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(new { settlement_history = response });
            }
            catch (InvalidParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthenticatedUserException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ExpenseNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while processing the expense share payment." });
            }
        }


        [HttpGet("{groupId}/expenses")]
        public async Task<IActionResult> GetGroupExpenses([FromRoute] GetExpensesForGroupQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(new { expenses = response });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while retrieving the expense details." });
            }
        }

        [HttpGet("{expenseId}")]
        public async Task<IActionResult> GetExpense([FromRoute] GetExpenseDetailsQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (InvalidParameterException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while retrieving the expense details." });
            }
        }

        [HttpGet("current/settlements")]
        public async Task<IActionResult> GetUserSettlements()
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

                var query = new GetUserSettlementsQuery { UserId = currentUserId };
                var response = await _mediator.Send(query);
                return Ok(new { settlements = response });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "An error occurred while retrieving settlement history." });
            }
        }

        [HttpGet("{groupId}/settlements")]
        public async Task<IActionResult> GetGroupSettlementHistory([FromRoute] GetSettlementHistoryForGroupQuery query)
        {
            try
            {
                //var query = new GetSettlementHistoryForGroupQuery { GroupId = groupId };
                var response = await _mediator.Send(query);
                return Ok(new { settlements = response });
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "An error occurred while retrieving settlement history for the group." });
            }
        }

    }
}
