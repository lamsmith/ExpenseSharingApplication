namespace ExpenseSharing.Domain.Exceptions
{
    public class ExpenseNotFoundException : DomainException
    {
        public ExpenseNotFoundException()
            : base($"Expense was not found.") { }
    }
}
