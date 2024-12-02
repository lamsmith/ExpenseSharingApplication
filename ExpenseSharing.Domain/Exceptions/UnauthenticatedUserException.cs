using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Domain.Exceptions
{
    public class UnauthenticatedUserException : DomainException
    {
        public UnauthenticatedUserException() : base("Unauthenticated user. Please log in to continue")
        {
            
        }
    }
}
