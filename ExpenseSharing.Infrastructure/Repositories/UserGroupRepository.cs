using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Infrastructure.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public UserGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserGroup> AddAsync(UserGroup userGroup)
        {
            await _context.UserGroups.AddAsync(userGroup);
            return userGroup;
        }

        public async Task<UserGroup> GetAsync(Expression<Func<UserGroup, bool>> predicate)
        {
            return await _context.UserGroups.FirstOrDefaultAsync(predicate);
        }

        public UserGroup RemoveAsync(UserGroup userGroup)
        {
            _context.UserGroups.Remove(userGroup);
            return userGroup;
        }

        public UserGroup UpdateAsync(UserGroup userGroup)
        {
            _context.UserGroups.Update(userGroup);
            return userGroup;
        }
    }
}
