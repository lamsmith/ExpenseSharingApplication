using ExpenseSharing.Domain.Common;

namespace ExpenseSharing.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "NGN";
        public Guid UserId { get; set; }
        
        
    }
}
