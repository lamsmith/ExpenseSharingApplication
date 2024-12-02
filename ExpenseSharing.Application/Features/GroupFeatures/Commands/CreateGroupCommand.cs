using MediatR;

namespace ExpenseSharing.Application.Features.GroupFeatures.Commands
{
    public class CreateGroupCommand : IRequest<Guid>
    {
        public string GroupName { get; set; }
    }
}
