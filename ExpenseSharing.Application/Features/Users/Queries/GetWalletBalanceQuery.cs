﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Features.Users.Queries
{
    public class GetWalletBalanceQuery : IRequest<decimal>
    {
        public Guid UserId { get; set; }
    }
}
