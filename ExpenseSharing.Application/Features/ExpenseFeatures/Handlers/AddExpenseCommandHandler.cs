using MediatR;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Application.Features.ExpenseFeatures.Commands;
using ExpenseSharing.Domain.Exceptions;
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;

namespace ExpenseFeatures.Handlers
{
    public class AddExpenseCommandHandler : IRequestHandler<AddExpenseCommand, Guid>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISettlementRepository _settlementRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AddExpenseCommandHandler(IExpenseRepository expenseRepository, IGroupRepository groupRepository, IUserRepository userRepository,  IUserService userService, IUnitOfWork unitOfWork, IUserGroupRepository userGroupRepository, ISettlementRepository settlementRepository)
        {
            _expenseRepository = expenseRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _settlementRepository = settlementRepository;

        }

        public async Task<Guid> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId) ?? throw new GroupNotFoundException();

            var expenseExists = await _groupRepository.IsGroupExists(x => x.Expenses.Any(x => x.Description == request.Description && x.GroupId == request.GroupId));
            if (expenseExists)
                throw new InvalidParameterException($"An expense with this description already exists in this group");

            var loggedInUser = await _userService.GetLoggedInUserAsync();
            
            if (group.CreatedBy != loggedInUser.Id)
                throw new InvalidParameterException($"Only the Group Admin can create an expense");

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                GroupId = request.GroupId,
                Description = request.Description,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUser.Id
            };
            

            foreach (var item in request.SplitDetails)
            {
                var user = await _userRepository.GetByIdAsync(item.Key) ?? throw new InvalidParameterException("Ensure all group members have been registered before splitting expense");
                var userExpense = new Settlement
                {
                    ExpenseId = expense.Id,
                    Expense = expense,
                    UserId = user.Id,
                    Amount = item.Value,
                };
                await _settlementRepository.AddAsync(userExpense);
            }

            

            await _expenseRepository.AddAsync(expense);
         
            await _unitOfWork.SaveAsync();
            return expense.Id;
        }
    }
}
