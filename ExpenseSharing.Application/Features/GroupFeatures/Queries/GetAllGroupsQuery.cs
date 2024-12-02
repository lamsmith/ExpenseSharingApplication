using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseSharing.Application.DTO.Responses;

namespace ExpenseSharing.Application.Features.GroupFeatures.Queries
{
    public record GetAllGroupsQuery(bool UsePaging = true) : PageRequest, IRequest<PaginatedList<GroupResponseModel>>
    {
    }
}
