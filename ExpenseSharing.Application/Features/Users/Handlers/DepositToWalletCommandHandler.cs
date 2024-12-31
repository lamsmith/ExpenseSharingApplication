using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Features.Users.Commands;
using ExpenseSharing.Application.Interfaces.Repositories;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Enums;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

namespace ExpenseSharing.Application.Features.WalletFeatures.Handlers;

public class DepositToWalletCommandHandler : IRequestHandler<DepositToWalletCommand, decimal>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;

    public DepositToWalletCommandHandler(IWalletRepository walletRepository, IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _walletRepository = walletRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository; 
    }

    public async Task<decimal> Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
            throw new InvalidParameterException("Amount must be greater than zero");

        var user = await _userRepository.GetByIdAsync(request.UserId)
            ?? throw new UserNotFoundException();

        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);

        if (wallet == null)
            throw new WalletNotFoundException($"Wallet for User ID {request.UserId} does not exist");

        // Update wallet balance
        wallet.Balance += request.Amount;
        await _walletRepository.UpdateAsync(wallet);

        // Create and save the transaction
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            CreatedAt = DateTime.UtcNow,
            Type = TransactionType.Credit,
            Narration = "Deposit transaction",
            WalletId = wallet.Id,
        };

        await _transactionRepository.AddAsync(transaction);

        return wallet.Balance;
    }
}