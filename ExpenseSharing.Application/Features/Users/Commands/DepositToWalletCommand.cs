using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Features.Users.Commands
{
    public class DepositToWalletCommand : IRequest<decimal>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
