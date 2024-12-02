using ExpenseSharing.Application.DTO.Responses;
using MediatR;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSharing.Application.Features.Expenses.Queries
{
    public record GetExpensesForGroupQuery() : PageRequest, IRequest<PaginatedList<ExpenseResponseModel>>
    {
        [FromRoute]
        public Guid GroupId { get; set; }
    }
}
