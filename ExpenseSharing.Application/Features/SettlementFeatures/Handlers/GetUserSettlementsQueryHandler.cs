using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

public class GetUserSettlementsQueryHandler : IRequestHandler<GetUserSettlementsQuery, List<GetUserSettlementResonseModel>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;

    public GetUserSettlementsQueryHandler(ITransactionRepository transactionRepository, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }

    public async Task<List<GetUserSettlementResonseModel>> Handle(GetUserSettlementsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new UserNotFoundException();

        var transactions = await _transactionRepository.GetAllTranscationByWalletIdAsync(user.Wallet.Id);
        if (transactions == null || !transactions.Any())
            return new List<GetUserSettlementResonseModel>(); 
        return transactions.Select(transaction => new GetUserSettlementResonseModel
        {
            ExpenseId = transaction.ExpenseId ?? Guid.Empty, // Use Guid.Empty if ExpenseId is null
            Amount = transaction.Amount,
            Date = transaction.CreatedAt,
            Description = transaction.Narration ?? "No Description"
        }).ToList();
    }
}
