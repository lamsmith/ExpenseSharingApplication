using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseSharing.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<User> GetLoggedInUserAsync()
        {
            try
            {
                var loggedInUserId = _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                var loggedInUser = await _userRepository.GetByIdAsync(Guid.Parse(loggedInUserId)) ?? throw new UnauthenticatedUserException();
                return loggedInUser;
            }
            catch(Exception)
            {
                throw new UnauthenticatedUserException();
            }
        }
    }
}
