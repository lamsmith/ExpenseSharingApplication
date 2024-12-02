using MediatR;
using Microsoft.AspNetCore.Mvc;

public class RemoveUserFromGroupCommand : IRequest<bool>
{
    [FromRoute]
    public Guid GroupId { get; set; }

    [FromBody]
    public Guid UserId { get; set; }
}