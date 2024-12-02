namespace ExpenseSharing.Application.DTO.Responses;

public class SettlementResponseModel
{
    public Guid ExpenseId { get; set; }
    public string Expense { get; set; }
    public Guid GroupId { get; set; }
    public string Group { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountPaid { get; set; }
}