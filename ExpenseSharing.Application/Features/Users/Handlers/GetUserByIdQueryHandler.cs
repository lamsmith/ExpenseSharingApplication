using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Features.Users.Queries;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Exceptions;
using MediatR;


namespace ExpenseSharing.Application.Features.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseModel?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new UserNotFoundException();
            return new UserResponseModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Groups = user.Groups.Select(ug => new GroupResponseModel()
                {
                    Id = ug.GroupId,
                    Name = ug.Group.Name
                }).ToList()
            };
        }
    }
}