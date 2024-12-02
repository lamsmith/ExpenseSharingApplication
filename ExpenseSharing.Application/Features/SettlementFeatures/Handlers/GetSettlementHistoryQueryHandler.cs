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
            return settlements.ToPaginated(settlements.Items.Select(s =>s.ToSettlementResponse()));
        }
    }

    public class GetSettlementHistoryForUserQueryHandler : IRequestHandler<GetSettlementHistoryForUserQuery, PaginatedList<SettlementResponseModel>>
    {
        private readonly ISettlementRepository _settlementRepository;

        public GetSettlementHistoryForUserQueryHandler(ISettlementRepository settlementRepository)
        {
            _settlementRepository = settlementRepository;
        }

        public async Task<PaginatedList<SettlementResponseModel>> Handle(GetSettlementHistoryForUserQuery? request, CancellationToken cancellationToken)
        {
            var settlements = await _settlementRepository.GetAllAsync(x => x.UserId == request.UserId, request, request.UsePaging);
            return settlements.ToPaginated(settlements.Items.Select(s =>s.ToSettlementResponse()));;
        }
    }

    public class GetSettlementHistoryForExpenseQueryHandler : IRequestHandler<GetSettlementHistoryForExpenseQuery, PaginatedList<SettlementResponseModel>>
    {
        private readonly ISettlementRepository _settlementRepository;

        public GetSettlementHistoryForExpenseQueryHandler(ISettlementRepository settlementRepository)
        {
            _settlementRepository = settlementRepository;
        }

        public async Task<PaginatedList<SettlementResponseModel>> Handle(GetSettlementHistoryForExpenseQuery? request, CancellationToken cancellationToken)
        {
            var settlements = await _settlementRepository.GetAllAsync(x => x.ExpenseId == request.ExpenseId, request, request.UsePaging);
            return settlements.ToPaginated(settlements.Items.Select(s =>s.ToSettlementResponse()));
        }
    }
}
