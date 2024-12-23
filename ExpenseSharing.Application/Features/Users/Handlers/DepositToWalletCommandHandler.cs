using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Features.Users.Commands;
using ExpenseSharing.Application.Interfaces.Repositories;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

namespace ExpenseSharing.Application.Features.WalletFeatures.Handlers;

public class DepositToWalletCommandHandler : IRequestHandler<DepositToWalletCommand, decimal>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUserRepository _userRepository;

    public DepositToWalletCommandHandler(IWalletRepository walletRepository, IUserRepository userRepository)
    {
        _walletRepository = walletRepository;
        _userRepository = userRepository;
    }

    public async Task<decimal> Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
            throw new InvalidParameterException("Amount must be greater than zero");

        var user = await _userRepository.GetByIdAsync(request.UserId)
            ?? throw new UserNotFoundException();

        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);

        if (wallet == null)
        {
            throw new WalletNotFoundException($"Wallet for User ID {request.UserId} does not exist");

        }
        else
        {
            wallet.Balance += request.Amount;
            await _walletRepository.UpdateAsync(wallet);
        }

        return wallet.Balance;
    }
}