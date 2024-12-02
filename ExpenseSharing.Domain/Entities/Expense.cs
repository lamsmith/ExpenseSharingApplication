using ExpenseSharing.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ExpenseSharing.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public ICollection<Settlement> Settlements { get; set; } = [];
        public bool IsSettled { get;  set; } = false;
    }
}
