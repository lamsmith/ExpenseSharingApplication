using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.DTO.Responses
{
    public class MemberResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MemberResponseModel> Members { get; set; }
    }
}
