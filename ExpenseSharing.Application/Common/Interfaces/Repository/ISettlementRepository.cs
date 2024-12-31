using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using System.Linq.Expressions;


namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface ISettlementRepository
    {
        Task<Settlement> AddAsync(Settlement settlement);
        Task<Settlement> GetAsync(Expression<Func<Settlement, bool>> predicate);
        Task<List<Settlement>> GetSettlementsByGroupIdAsync(Guid groupId);
        public Settlement Update(Settlement settlement);

        Task<PaginatedList<Settlement>> GetAllAsync(PageRequest pageRequest, bool usePaging = true);
        Task<PaginatedList<Settlement>> GetAllAsync(Expression<Func<Settlement, bool>> expression, PageRequest? pageRequest, bool usePaging = true);

        Task<IEnumerable<Settlement>> GetSettlementsByIdsAsync(IEnumerable<Guid> settlementIds);

    }
}
