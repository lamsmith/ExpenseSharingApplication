using MediatR;

namespace ExpenseSharing.Application.Features.UserFeatures.Commands
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
