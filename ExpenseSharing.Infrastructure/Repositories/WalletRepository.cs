using ExpenseSharing.Application.Interfaces.Repositories;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseSharing.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WalletRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get wallet by its unique ID
        public async Task<Wallet> GetByIdAsync(Guid walletId)
        {
            return await _dbContext.Wallets
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

        // Get wallet by user ID (since each user has one wallet)
        public async Task<Wallet> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Wallets
                .FirstOrDefaultAsync(w => w.UserId == userId);  // Assuming UserId is part of Wallet
        }

        // Update wallet balance (or any other wallet properties)
        public async Task UpdateAsync(Wallet wallet)
        {
            _dbContext.Wallets.Update(wallet);
            await _dbContext.SaveChangesAsync();
        }
    }
}
