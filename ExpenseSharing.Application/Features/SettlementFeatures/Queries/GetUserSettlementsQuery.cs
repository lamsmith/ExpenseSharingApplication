using ExpenseSharing.Application.DTO.Responses;
using MediatR;

public class GetUserSettlementsQuery : IRequest<List<GetUserSettlementResonseModel>>
{
    public Guid UserId { get; set; }
}

