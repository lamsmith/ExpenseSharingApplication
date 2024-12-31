using ExpenseSharing.Application.Common.Exceptions;
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Enums;
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
            var transaction = await _dbContext.Transactions
         .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction == null)
                throw new NotFoundException($"Transaction with ID {transactionId} not found.");

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllTranscationByWalletIdAsync(Guid walletId)
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

        public async Task<IEnumerable<Transaction>> GetSettlementHistoryByExpenseIdAsync(Guid expenseId)
        {
            
            return await _dbContext.Transactions
                .Where(t => t.ExpenseId == expenseId && t.Type == TransactionType.Debit) 
                .ToListAsync();
        }


       

       


    }
}
