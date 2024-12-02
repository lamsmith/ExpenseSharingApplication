using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Domain.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Features.SettlementFeatures.Command
{
    public class CreateSettlementCommand : IRequest<SettlementResponseModel>
    {
        [FromRoute]
        public Guid ExpenseId { get; set; }
       
        [FromBody]
        public SettlementDto Settlement { get; set; }
    }

    public class SettlementDto
    {
        public decimal Amount { get; set; }
    }
}
