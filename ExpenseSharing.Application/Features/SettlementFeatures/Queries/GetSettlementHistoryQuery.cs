using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseSharing.Application.DTO;
using ExpenseSharing.Application.DTO.Responses;

namespace ExpenseSharing.Application.Features.SettlementFeatures.Queries
{
    public record GetSettlementHistoryQuery() : PageRequest, IRequest<PaginatedList<SettlementResponseModel>>
    {
    }

    public record GetSettlementHistoryForUserQuery() : PageRequest, IRequest<PaginatedList<SettlementResponseModel>>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }

    public record GetSettlementHistoryForExpenseQuery() : PageRequest, IRequest<PaginatedList<SettlementResponseModel>>
    {
        [FromRoute]
        public Guid ExpenseId { get; set; }
    }
}
