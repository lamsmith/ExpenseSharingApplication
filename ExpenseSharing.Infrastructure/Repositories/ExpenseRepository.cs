using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using ExpenseSharing.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseSharing.Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Expense>> GetAllAsync(Expression<Func<Expense, bool>> exp, PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Expenses.Include(e => e.Group).Where(exp);

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

        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Expenses.Include(e => e.Group).FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                // Log the error
                throw new Exception("Error retrieving the expense.", ex);
            }

        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
        }

        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
            }
        }
    }
}
