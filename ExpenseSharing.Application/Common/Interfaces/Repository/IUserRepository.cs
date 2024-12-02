using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;

namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<PaginatedList<User>> GetAllAsync(PageRequest pageRequest, bool usePaging = true);
        bool IsExitByEmail(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<Guid> userIds);
    }

}
