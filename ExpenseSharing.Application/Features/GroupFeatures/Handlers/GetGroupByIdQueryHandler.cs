using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Extensions;
using ExpenseSharing.Application.Features.GroupFeatures.Queries;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupResponseModel?>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupByIdQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GroupResponseModel?> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        if (group == null)
            throw new GroupNotFoundException();
        return group.ToGroupResponse();
    }
}