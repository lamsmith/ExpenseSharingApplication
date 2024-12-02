using ExpenseSharing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface IUserGroupRepository
    {
        Task<UserGroup> AddAsync(UserGroup userGroup);
        Task<UserGroup> GetAsync(Expression<Func<UserGroup, bool>> predicate);
        UserGroup UpdateAsync(UserGroup userGroup);
        UserGroup RemoveAsync(UserGroup userGroup);
    }
}
