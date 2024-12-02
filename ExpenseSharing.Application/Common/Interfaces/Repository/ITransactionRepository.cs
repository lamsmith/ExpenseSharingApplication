using ExpenseSharing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<Transaction> GetByIdAsync(Guid transactionId);
        Task<IEnumerable<Transaction>> GetByWalletIdAsync(Guid walletId);
        Task<IEnumerable<Transaction>> GetByExpenseIdAsync(Guid expenseId);
    }
}
