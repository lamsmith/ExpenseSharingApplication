using ExpenseSharing.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseSharing.Application.DTO.Responses;

namespace ExpenseSharing.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponseModel?>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }
}
