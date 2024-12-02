namespace ExpenseSharing.Application.DTO.Responses;

public class ExpenseResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}