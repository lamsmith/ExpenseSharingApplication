using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using System.Linq.Expressions;


namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface IExpenseRepository
    {
        Task<PaginatedList<Expense>> GetAllAsync(Expression<Func<Expense, bool>> exp, PageRequest pageRequest, bool usePaging = true);
        Task<Expense?> GetByIdAsync(Guid id);
        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Guid id);
    }
}
