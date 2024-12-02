using ExpenseSharing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Infrastructure.Persistence.Configurations
{
    public class SettlementConfiguration : IEntityTypeConfiguration<Settlement>
    {
        public void Configure(EntityTypeBuilder<Settlement> builder)
        {

            // Settlement to Expense relationship
            builder.HasOne(s => s.Expense)
                   .WithMany(e => e.Settlements)
                   .HasForeignKey(s => s.ExpenseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Settlements");
        }
    }
}
