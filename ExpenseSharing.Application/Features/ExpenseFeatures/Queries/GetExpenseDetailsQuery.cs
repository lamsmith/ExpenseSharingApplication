using MediatR;

namespace ExpenseSharing.Application.Features.Expenses.Queries;

public record GetExpenseDetailsQuery : IRequest<ExpenseDetailsResponse>
{
    public Guid ExpenseId { get; init; }
}

public record ExpenseDetailsResponse
{
    public Guid ExpenseId { get; init; }
    public string Description { get; init; }
    public decimal Amount { get; init; }
    public decimal AmountLeft { get; init; }
    public SettlementDetails Settlement { get; init; }
}

public record SettlementDetails
{
    public Guid UserId { get; init; }
    public string UserFirstName { get; init; }
    public string UserLastName { get; init; }
    public decimal Amount { get; init; }
    public decimal AmountPaid { get; init; }
    public decimal AmountLeft { get; init; }
}