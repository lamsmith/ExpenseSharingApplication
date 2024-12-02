using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Domain.Exceptions
{
    public class UnauthorizedUserAccessException : DomainException
    {
        public UnauthorizedUserAccessException() : base("User does not have enough authority to access this resource")
        {
            
        }
    }
}
