using Microsoft.EntityFrameworkCore;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using ExpenseSharing.Infrastructure.Persistence.Context;
using ExpenseSharing.Application.Common.Interfaces.Repository;

namespace ExpenseSharing.Infrastructure.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private const string GroupCacheKeyPrefix = "GroupsForUser:";


        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Group>> GetAllAsync(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Groups.Include(g => g.Expenses).AsQueryable();

            query = query.OrderBy(r => r.Name);

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

        public async Task<Group?> GetByIdAsync(Guid id)
        {
            return await _context.Groups.Include(g => g.Expenses)
                .Include(g => g.Members)
                .ThenInclude(g => g.User)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
            }
        }

        public async Task<bool> IsGroupExists(Expression<Func<Group, bool>> exp)
        {
            var exists = await _context.Groups.AnyAsync(exp);
            return exists;
        }

        public async Task<PaginatedList<Group>> GetAllAsync(Expression<Func<Group, bool>> exp, PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Groups
                .Include(g => g.Members)
                .ThenInclude(g => g.User)
                .Where(exp);

            query = query.OrderBy(r => r.Name);

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
    }
}
