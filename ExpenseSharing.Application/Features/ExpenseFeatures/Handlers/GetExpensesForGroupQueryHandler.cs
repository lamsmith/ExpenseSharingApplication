using MediatR;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Application.Features.Expenses.Queries;
using ExpenseSharing.Domain.Paging;
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Extensions;
using ExpenseSharing.Domain.Exceptions;

namespace ExpenseSharing.Application.Features.Expenses.Handlers
{
    public class GetExpensesForGroupQueryHandler : IRequestHandler<GetExpensesForGroupQuery, PaginatedList<ExpenseResponseModel>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserService _userService;

        public GetExpensesForGroupQueryHandler(IExpenseRepository expenseRepository, IGroupRepository groupRepository, IUserGroupRepository userGroupRepository, IUserService userService)
        {
            _expenseRepository = expenseRepository;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
            _userService = userService;
        }

        public async Task<PaginatedList<ExpenseResponseModel>> Handle(GetExpensesForGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null)
                throw new InvalidParameterException("Invalid group");

            var loggedUser = await _userService.GetLoggedInUserAsync(); 

            var userGroup = await _userGroupRepository.GetAsync(x => x.GroupId == request.GroupId && x.UserId == loggedUser.Id);
            if (userGroup == null)
                throw new InvalidParameterException("Invalid Group"); //not a member of the group
            
            var allExpenses = await _expenseRepository.GetAllAsync(e => e.GroupId == request.GroupId,request, request.UsePaging);
            return allExpenses.ToPaginated(allExpenses.Items.Select(s => s.ToExpenseResponse()));
        }
    }
}
