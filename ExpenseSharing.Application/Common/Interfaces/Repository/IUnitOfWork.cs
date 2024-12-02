
namespace ExpenseSharing.Application.Common.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
