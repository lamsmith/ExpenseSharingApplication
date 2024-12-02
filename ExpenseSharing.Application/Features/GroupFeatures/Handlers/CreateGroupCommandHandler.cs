using MediatR;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Application.Features.GroupFeatures.Commands;
using ExpenseSharing.Domain.Exceptions;
using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;

namespace ExpenseSharing.Application.Features.GroupFeatures.Handlers
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public CreateGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork, IUserService userService, IUserGroupRepository userGroupRepository)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
            _userService = userService;
            _userGroupRepository = userGroupRepository;
        }

        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var groupExists = await _groupRepository.IsGroupExists(x => x.Name == request.GroupName);
            if (groupExists)
                throw new InvalidParameterException($"{request.GroupName} group already exists");
            
            var loggedInUser = await _userService.GetLoggedInUserAsync();

            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = request.GroupName,
                CreatedBy = loggedInUser.Id,
                CreatedAt = DateTime.UtcNow
            };

            var userGroup = new UserGroup
            {
                GroupId = group.Id,
                Group = group,
                UserId = loggedInUser.Id
            };

            await _groupRepository.AddAsync(group);
            await _userGroupRepository.AddAsync(userGroup);
            await _unitOfWork.SaveAsync();
            return group.Id;
        }
    }
}
