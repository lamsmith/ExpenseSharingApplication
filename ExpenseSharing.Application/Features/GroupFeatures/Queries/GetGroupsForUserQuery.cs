using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public record GetGroupsForUserQuery : PageRequest, IRequest<PaginatedList<GroupResponseModel>>
{
    public bool UsePaging { get; init; } = true;

    [FromRoute]
    public Guid UserId { get; init; }
}
