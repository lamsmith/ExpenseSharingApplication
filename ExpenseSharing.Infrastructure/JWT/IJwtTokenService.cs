using System.Security.Claims;

namespace ExpenseSharing.Infrastructure.JWT
{
    public interface IJwtTokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
