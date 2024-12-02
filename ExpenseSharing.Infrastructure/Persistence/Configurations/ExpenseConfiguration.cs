using ExpenseSharing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseSharing.Infrastructure.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(e => e.Amount)
                   .IsRequired();

            builder.HasOne(e => e.Group)
                   .WithMany(g => g.Expenses)
                   .HasForeignKey(e => e.GroupId);

            builder.ToTable("Expenses");
        }
    }
}
