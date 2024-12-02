using ExpenseSharing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.DTO
{
    public  class GroupResponseModel
    {
       
        public ICollection<User> Members { get; set; } 
        public ICollection<Expense> Expenses { get; set; } 
    }
}
