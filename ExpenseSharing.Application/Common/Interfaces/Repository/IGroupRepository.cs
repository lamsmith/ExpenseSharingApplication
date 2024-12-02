using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using System.Linq.Expressions;

namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface IGroupRepository
    {
        Task<PaginatedList<Group>> GetAllAsync(PageRequest pageRequest, bool usePaging = true);
        Task<PaginatedList<Group>> GetAllAsync(Expression<Func<Group, bool>> exp, PageRequest pageRequest, bool usePaging = true);
        Task<Group?> GetByIdAsync(Guid id);
        Task<bool> IsGroupExists(Expression<Func<Group, bool>> exp);
        Task AddAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
    }
}
