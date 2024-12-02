using ExpenseSharing.Domain.Common;

namespace ExpenseSharing.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<UserGroup> Groups { get; set; } = [];
    }
}
