using ExpenseSharing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Common.Interfaces.Service
{
    public interface IUserService
    {
        Task<User> GetLoggedInUserAsync();
    }
}
