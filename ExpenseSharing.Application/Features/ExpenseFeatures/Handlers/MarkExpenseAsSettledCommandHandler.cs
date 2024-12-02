using MediatR;
using ExpenseSharing.Domain.Exceptions;
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;

namespace ExpenseFeatures.Handlers
{
    public class MarkExpenseAsSettledCommandHandler : IRequestHandler<MarkExpenseAsSettledCommand, bool>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserService _userService;

        public MarkExpenseAsSettledCommandHandler(IExpenseRepository expenseRepository, IUserService userService)
        {
            _expenseRepository = expenseRepository;
            _userService = userService;
        }

        public async Task<bool> Handle(MarkExpenseAsSettledCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);

            if (expense == null)
                throw new ExpenseNotFoundException();

            var loggedInUser = await _userService.GetLoggedInUserAsync();

            expense.IsSettled = true;
            expense.UpdatedAt = DateTime.UtcNow;
            expense.UpdatedBy = loggedInUser.Email;
            await _expenseRepository.UpdateAsync(expense);
            return true;
        }
    }
}
