
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Features.Expenses.Queries;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

namespace ExpenseSharing.Application.Features.Expenses.Handlers;

public class GetExpenseDetailsQueryHandler : IRequestHandler<GetExpenseDetailsQuery, ExpenseDetailsResponse>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ISettlementRepository _settlementRepository;
    private readonly IUserRepository _userRepository;

    public GetExpenseDetailsQueryHandler(
        IExpenseRepository expenseRepository,
        ISettlementRepository settlementRepository,
        IUserRepository userRepository)
    {
        _expenseRepository = expenseRepository;
        _settlementRepository = settlementRepository;
        _userRepository = userRepository;
    }

    public async Task<ExpenseDetailsResponse> Handle(GetExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId)
                      ?? throw new InvalidParameterException($"Expense with ID {request.ExpenseId} not found.");

        var settlementItems = await _settlementRepository.GetAllAsync(s => s.ExpenseId == request.ExpenseId, null, false);
        var settlements = settlementItems.Items;
        
        var userIds = settlements.Select(s => s.UserId).Distinct();
        var users = await _userRepository.GetUsersByIdsAsync(userIds);
        
        var userDict = users.ToDictionary(u => u.Id);

        var settlementDetails = settlements.Select(s => new SettlementDetails
        {
            UserId = s.UserId,
            UserFirstName = userDict[s.UserId].FirstName,
            UserLastName = userDict[s.UserId].LastName,
            Amount = s.Amount,
            AmountPaid = s.AmountPaid,
            AmountLeft = s.Amount - s.AmountPaid
        });

        return new ExpenseDetailsResponse
        {
            ExpenseId = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            AmountLeft = expense.Amount - settlements.Sum(s => s.AmountPaid),
            Settlements = settlementDetails
        };
    }
}