using MediatR;
using Microsoft.AspNetCore.Mvc;

public class MarkExpenseAsSettledCommand : IRequest<bool>
{
    [FromRoute]
    public Guid ExpenseId { get; set; }
}
