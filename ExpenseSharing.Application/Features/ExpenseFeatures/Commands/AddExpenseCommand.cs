using MediatR;

namespace ExpenseSharing.Application.Features.ExpenseFeatures.Commands
{
    public class AddExpenseCommand : IRequest<Guid>
    {
        public Guid GroupId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Dictionary<Guid, decimal> SplitDetails { get; set; } = new();
    }
}