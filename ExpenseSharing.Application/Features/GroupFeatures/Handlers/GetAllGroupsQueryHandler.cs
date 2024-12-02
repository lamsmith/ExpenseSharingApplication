using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Extensions;
using ExpenseSharing.Application.Features.GroupFeatures.Queries;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using MediatR;

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, PaginatedList<GroupResponseModel>>
{
    private readonly IGroupRepository _groupRepository;

    public GetAllGroupsQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<PaginatedList<GroupResponseModel>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetAllAsync(request, request.UsePaging);
        return groups.ToPaginated(groups.Items.Select(g => g.ToGroupResponse()));
    }
}