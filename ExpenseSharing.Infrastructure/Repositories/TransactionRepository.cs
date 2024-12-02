using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseSharing.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Transaction> GetByIdAsync(Guid transactionId)
        {
            return await _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetByWalletIdAsync(Guid walletId)
        {
            return await _dbContext.Transactions
                .Where(t => t.WalletId == walletId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByExpenseIdAsync(Guid expenseId)
        {
            return await _dbContext.Transactions
                .Where(t => t.ExpenseId == expenseId)
                .ToListAsync();
        }
    }
}
