namespace ExpenseSharing.Application.DTO.Responses
{
    public class GetUserSettlementResonseModel
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
