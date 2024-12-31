
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Application.Features.Expenses.Queries;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

namespace ExpenseSharing.Application.Features.Expenses.Handlers;

public class GetExpenseDetailsQueryHandler : IRequestHandler<GetExpenseDetailsQuery, ExpenseDetailsResponse>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ISettlementRepository _settlementRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetExpenseDetailsQueryHandler(
        IExpenseRepository expenseRepository,
        ISettlementRepository settlementRepository,
        IUserRepository userRepository,
        IUserService userService)
    {
        _expenseRepository = expenseRepository;
        _settlementRepository = settlementRepository;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ExpenseDetailsResponse> Handle(GetExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId)
                      ?? throw new InvalidParameterException($"Expense with ID {request.ExpenseId} not found.");

        var loginUser = await _userService.GetLoggedInUserAsync();

        var settlement = await _settlementRepository.GetAsync(s => s.ExpenseId == request.ExpenseId && s.UserId == loginUser.Id);

        if (settlement == null)
        {
            throw new InvalidParameterException("error");
        }


            var settlementDetails = new SettlementDetails
        {
            UserId = loginUser.Id,
            UserFirstName = loginUser.FirstName,
            UserLastName = loginUser.LastName,
            Amount = settlement.Amount,
            AmountPaid = settlement.AmountPaid,
            AmountLeft = settlement.Amount - settlement.AmountPaid
        };

      

        return new ExpenseDetailsResponse
        {
            ExpenseId = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            AmountLeft = expense.Amount - settlement.AmountPaid,
            Settlement = settlementDetails
        };
    }
}