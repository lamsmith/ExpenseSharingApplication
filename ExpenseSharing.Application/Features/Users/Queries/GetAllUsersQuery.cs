using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Features.Users.Queries
{
    public record GetAllUsersQuery(bool UsePaging = true) : PageRequest, IRequest<PaginatedList<User>>
    {

    }
}
