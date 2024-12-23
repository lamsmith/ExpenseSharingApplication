using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Features.Users.Queries;
using ExpenseSharing.Application.Interfaces.Repositories;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

namespace ExpenseSharing.Application.Features.WalletFeatures.Handlers;

public class GetWalletBalanceQueryHandler : IRequestHandler<GetWalletBalanceQuery, decimal>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUserRepository _userRepository;

    public GetWalletBalanceQueryHandler(IWalletRepository walletRepository, IUserRepository userRepository)
    {
        _walletRepository = walletRepository;
        _userRepository = userRepository;
    }

    public async Task<decimal> Handle(GetWalletBalanceQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId)
            ?? throw new UserNotFoundException();

        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);

        return wallet?.Balance ?? 0;
    }
}