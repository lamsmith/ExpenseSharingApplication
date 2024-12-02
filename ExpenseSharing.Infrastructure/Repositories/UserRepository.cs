using Microsoft.EntityFrameworkCore;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using ExpenseSharing.Infrastructure.Persistence.Context;
using ExpenseSharing.Application.Common.Interfaces.Repository;

namespace ExpenseSharing.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<User>> GetAllAsync(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Users.Include(u => u.Groups).ThenInclude(u => u.Group).AsQueryable();

            query = query.OrderBy(r => r.LastName);

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

        public bool IsExitByEmail(string email) => _context.Users.Any(s => s.Email   == email);

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.Groups).ThenInclude(u => u.Group).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task AddAsync(User user)
        {
             await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
               
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<Guid> userIds)
        {
            return await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
        }
    }
}
