using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, bool>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    public RemoveUserFromGroupCommandHandler(IGroupRepository groupRepository, IUserGroupRepository userGroupRepository, IUnitOfWork unitOfWork, IUserService userService)
    {
        _groupRepository = groupRepository;
        _userGroupRepository = userGroupRepository;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task<bool> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);

        if (group == null)
            throw new InvalidParameterException("Invalid group");

        var loggedUser = await _userService.GetLoggedInUserAsync();

        if (loggedUser.Id != group.CreatedBy)
            throw new InvalidParameterException("You are not allowed to remove users from this group");


        var userGroup = await _userGroupRepository.GetAsync(x => x.GroupId == request.GroupId && x.UserId == request.GroupId);
        if (userGroup == null)
            throw new InvalidParameterException("User not found in group");

        _userGroupRepository.RemoveAsync(userGroup);
        await _unitOfWork.SaveAsync();
        return true;
    }
}