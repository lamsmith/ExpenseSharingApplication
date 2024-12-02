﻿using MediatR;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Extensions;

namespace ExpenseSharing.Application.Features.GroupFeatures.Handlers
{
    public class GetGroupsForUserQueryHandler : IRequestHandler<GetGroupsForUserQuery, PaginatedList<GroupResponseModel>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsForUserQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<PaginatedList<GroupResponseModel>> Handle(GetGroupsForUserQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAllAsync(x => x.Members.Any(m => m.UserId == request.UserId), request, request.UsePaging);

            return groups.ToPaginated(groups.Items.Select(g => g.ToGroupResponse()));
        }
    }
}