using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Domain.Exceptions
{
    public class InvalidParameterException : DomainException
    {
        public InvalidParameterException(string message) : base(message)
        {
            
        }
    }
}
