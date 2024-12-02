using ExpenseSharing.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseSharing.Application.DTO.Responses;

namespace ExpenseSharing.Application.Features.GroupFeatures.Queries
{
    public class GetGroupByIdQuery : IRequest<GroupResponseModel?>
    {
        [FromRoute]
        public Guid GroupId { get; set; }
    }
}
