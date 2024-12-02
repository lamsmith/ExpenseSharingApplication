using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Domain.Entities;

namespace ExpenseSharing.Application.Common.Interfaces.Service
{
    public interface IAuthService
    {
        Task RegisterAsync(UserRegistrationRequestModel request);
        Task<UserLoginResponseModel> LoginAsync(UserLoginRequestModel request);
        Task LogoutAsync();
    }
}
