using Microsoft.AspNetCore.Mvc;

namespace ExpenseSharing.Application.Features.Expenses.Queries;

public class GetExpensesForUserQuery
{
    [FromRoute]
    public Guid UserId { get; set; }
}