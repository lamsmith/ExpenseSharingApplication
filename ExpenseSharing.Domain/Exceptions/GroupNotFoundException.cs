namespace ExpenseSharing.Domain.Exceptions
{
    public class GroupNotFoundException : DomainException
    {
        public GroupNotFoundException()
            : base($"Group was not found.") { }
    }
}
