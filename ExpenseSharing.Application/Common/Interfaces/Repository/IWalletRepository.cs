using System;
using System.Threading.Tasks;
using ExpenseSharing.Domain.Entities;

namespace ExpenseSharing.Application.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetByIdAsync(Guid walletId);
        Task UpdateAsync(Wallet wallet);
        Task<Wallet> GetByUserIdAsync(Guid userId); // To get the wallet by user ID
    }
}
