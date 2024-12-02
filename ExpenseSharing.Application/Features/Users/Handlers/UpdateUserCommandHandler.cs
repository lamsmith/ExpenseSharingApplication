using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Features.Users.Commands;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return false;

        var emailExists = _userRepository.IsExitByEmail(request.Email);
        if (emailExists)
            throw new InvalidParameterException("Email inputted already exists");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await _userRepository.UpdateAsync(user);

        return true;
    }
}