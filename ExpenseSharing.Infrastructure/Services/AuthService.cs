using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Domain.Enums;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionRepository _transactionRepository;

    public AuthService(IUserRepository userRepository,
                       IConfiguration configuration,
                       IHttpContextAccessor httpContextAccessor,
                     ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _transactionRepository = transactionRepository;

    }

    public async Task RegisterAsync(UserRegistrationRequestModel request)
    {
        var existingUser = _userRepository.IsExitByEmail(request.Email);
       if (existingUser )
        {
            throw new ArgumentException("A user with the same email already exists.");
        }

        // Create user and hash passwood
        var salt = GenerateSalt();
        var hashedPassword = HashPasswordWithSalt(request.Password, salt);

         var wallet = new Wallet
         {
             Id = Guid.NewGuid(),
             Balance = request.WalletAmount,
             Currency = "NGN"
         };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = hashedPassword,
            PasswordSalt = salt,
            Wallet = wallet
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            WalletId = wallet.Id,
            Amount = request.WalletAmount,
            Narration = "Initial wallet deposit",
            Type = TransactionType.Credit,
            CreatedAt = DateTime.UtcNow
        };


            await _userRepository.AddAsync(user); 
            await _transactionRepository.AddAsync(transaction);    
    }

    public async Task<UserLoginResponseModel> LoginAsync(UserLoginRequestModel request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }
        
        var token =  await GenerateTokenAsync(user);

        return new UserLoginResponseModel()
        {
            Firstname = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Token = token
        };
    }

    public Task LogoutAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        httpContext?.Response.Cookies.Delete("AuthToken");
        return Task.CompletedTask;
    }

    public Task<string> GenerateTokenAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token validity
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return Task.FromResult(token);
    }

    private string GenerateSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPasswordWithSalt(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedPassword = password + salt;
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var hash = HashPasswordWithSalt(password, storedSalt);
        return hash == storedHash;
    }
}
