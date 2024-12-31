using MediatR;
using ExpenseSharing.Domain.Entities;

namespace ExpenseSharing.Application.Features.SettlementFeatures.Queries
{
    public class GetSettlementHistoryForGroupQuery : IRequest<List<Transaction>>
    {
        
        public Guid GroupId { get; set; }
    }
}
