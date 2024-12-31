using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.DTO;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Extensions;
using ExpenseSharing.Application.Features.SettlementFeatures.Queries;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using MediatR;

namespace ExpenseSharing.Application.Features.SettlementFeatures.Handlers
{
    public class GetSettlementHistoryQueryHandler : IRequestHandler<GetSettlementHistoryQuery, PaginatedList<SettlementResponseModel>>
    {
        private readonly ISettlementRepository _settlementRepository;

        public GetSettlementHistoryQueryHandler(ISettlementRepository settlementRepository)
        {
            _settlementRepository = settlementRepository;
        }

        public async Task<PaginatedList<SettlementResponseModel>> Handle(GetSettlementHistoryQuery request, CancellationToken cancellationToken)
        {
            var settlements = await _settlementRepository.GetAllAsync(request, request.UsePaging);
            return settlements.ToPaginated(settlements.Items.Select(s => s.ToSettlementResponse()));
        }
    }


}
