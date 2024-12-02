using MediatR;
using ExpenseSharing.Application.Features.Users.Commands;
using ExpenseSharing.Application.Common.Interfaces.Repository;

namespace UserFeatures.Commands.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return false;

            await _userRepository.DeleteAsync(request.UserId);

            return true;
        }
    }
}
