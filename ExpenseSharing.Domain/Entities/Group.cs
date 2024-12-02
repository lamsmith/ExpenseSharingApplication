using ExpenseSharing.Domain.Common;

namespace ExpenseSharing.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserGroup> Members { get; set; } = [];
        public ICollection<Expense> Expenses { get; set; } = [];
    }
}
