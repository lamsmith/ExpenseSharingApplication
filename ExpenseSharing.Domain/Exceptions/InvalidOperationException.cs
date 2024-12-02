namespace ExpenseSharing.Domain.Exceptions
{
    public class InvalidOperationException : DomainException
    {
        public InvalidOperationException(string message)
            : base($"Invalid operation: {message}") { }
    }
}
