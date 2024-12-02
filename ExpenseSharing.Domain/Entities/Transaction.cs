using ExpenseSharing.Domain.Common;
using ExpenseSharing.Domain.Enums;

namespace ExpenseSharing.Domain.Entities;

public class Transaction : BaseEntity
{
    public TransactionType Type { get; set; }
    
    public decimal Amount { get; set; }

    public Guid WalletId { get; set; }

    public Guid? ExpenseId { get; set; }

    public string Narration { get; set; }
}