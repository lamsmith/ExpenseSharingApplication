using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Domain.Exceptions
{
    public class RoleNotFoundException : DomainException
    {
        public RoleNotFoundException(string roleName) : base($"{roleName} role not found")
        {
            
        }
    }
}
