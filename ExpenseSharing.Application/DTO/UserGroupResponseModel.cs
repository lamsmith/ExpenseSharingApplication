﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.DTO
{
    public class UserGroupResponseModel
    {
        public Guid UserId { get; set; } 
        public string Email { get; set; } 
    }
}
