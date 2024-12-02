namespace ExpenseSharing.Domain.Exceptions
{
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException()
            : base($"User was not found.") { }
    }
}
