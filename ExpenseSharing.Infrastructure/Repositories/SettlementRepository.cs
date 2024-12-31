using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using ExpenseSharing.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseSharing.Infrastructure.Repositories
{
    public class SettlementRepository : ISettlementRepository
    {
        private readonly ApplicationDbContext _context;

        public SettlementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Settlement> AddAsync(Settlement settlement)
        {
            await _context.Settlements.AddAsync(settlement);
            return settlement;
        }

        public async Task<Settlement> GetAsync(Expression<Func<Settlement, bool>> predicate)
        {
            return await _context.Settlements.Include(s => s.Expense).FirstOrDefaultAsync(predicate);   
        }

        public async Task<PaginatedList<Settlement>> GetAllAsync(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Settlements.AsQueryable();

            query = query.OrderBy(r => r.CreatedAt);

            var totalItemsCount = await query.CountAsync();
            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }
        }

        public async Task<PaginatedList<Settlement>> GetAllAsync(Expression<Func<Settlement, bool>> expression, PageRequest? pageRequest, bool usePaging = true)
        {
            var query = _context.Settlements.Include(s => s.Expense).Where(expression);

            query = query.OrderBy(r => r.CreatedAt);

            var totalItemsCount = await query.CountAsync();
            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }
        }

         public Settlement Update (Settlement settlement)
        {
           _context.Settlements.Update(settlement);

            return settlement;

        }

        public async Task<IEnumerable<Settlement>> GetSettlementsByIdsAsync(IEnumerable<Guid> settlementIds)
        {
            return await _context.Settlements.Include(s => s.Expense).Where(s => settlementIds.Contains(s.Id)).ToListAsync();
        }


        public async Task<List<Settlement>> GetSettlementsByGroupIdAsync(Guid groupId)
        {
            return await _context.Settlements
                .Include(s => s.Expense)
                .ThenInclude(e => e.Group)
                .Where(s => s.Expense.GroupId == groupId)
                .ToListAsync();
        }
    }
}
