using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand, bool>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public AddUserToGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository, IUserGroupRepository userGroupRepository, IUnitOfWork unitOfWork, IUserService userService)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _userGroupRepository = userGroupRepository;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task<bool> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        var user = await _userRepository.GetByEmailAsync(request.Email);

        var loggedUser = await _userService.GetLoggedInUserAsync();

        if (group == null || user == null)
            throw new InvalidParameterException("Invalid group or user");

        if (loggedUser.Id != group.CreatedBy)
            throw new InvalidParameterException("You are not allowed to add users to this group");

        var userGroup = new UserGroup
        {
            GroupId = group.Id,
            UserId = user.Id
        };

        await _userGroupRepository.AddAsync(userGroup);
        await _unitOfWork.SaveAsync();
        return true;
    }
}