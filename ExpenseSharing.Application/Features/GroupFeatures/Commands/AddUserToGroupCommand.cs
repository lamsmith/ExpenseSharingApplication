using MediatR;
using Microsoft.AspNetCore.Mvc;

public class AddUserToGroupCommand : IRequest<bool>
{
    [FromRoute]
    public Guid GroupId { get; set; }

    [FromBody]
    public string Email { get; set; }
}