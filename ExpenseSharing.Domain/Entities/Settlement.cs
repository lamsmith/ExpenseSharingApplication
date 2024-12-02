using ExpenseSharing.Domain.Common;

namespace ExpenseSharing.Domain.Entities
{
    public class Settlement : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ExpenseId { get; set; }
        public Expense Expense { get; set; }
        public decimal Amount { get; set; }

        public decimal AmountPaid { get; set; }
    }
}
