using ExpenseSharing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseSharing.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Define the primary key
            builder.HasKey(t => t.Id);

            // Configure properties
            builder.Property(t => t.Type)
                   .IsRequired();

            builder.Property(t => t.Amount)
                   .HasColumnType("decimal(18,2)") 
                   .IsRequired();

            builder.Property(t => t.Narration)
                   .HasMaxLength(500)
                   .IsRequired(false); 

            // Configure Wallet foreign key
            builder.HasOne<Wallet>()
                   .WithMany()
                   .HasForeignKey(t => t.WalletId)
                   .OnDelete(DeleteBehavior.Cascade); 

            // Configure Expense foreign key
            builder.HasOne<Expense>()
                   .WithMany()
                   .HasForeignKey(t => t.ExpenseId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Map entity to table
            builder.ToTable("Transactions");
        }
    }
}
