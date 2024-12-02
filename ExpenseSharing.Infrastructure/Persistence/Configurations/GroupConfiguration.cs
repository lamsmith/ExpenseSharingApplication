using ExpenseSharing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseSharing.Infrastructure.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            // Define Primary Key
            builder.HasKey(g => g.Id);

            // Configure Name Property
            builder.Property(g => g.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            // Configure Expenses Relationship
            builder.HasMany(g => g.Expenses)
                   .WithOne(e => e.Group)
                   .HasForeignKey(e => e.GroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Map BaseEntity Properties (if not automatic)
            builder.Property(g => g.CreatedBy).IsRequired();
            builder.Property(g => g.CreatedAt).IsRequired();
            builder.Property(g => g.UpdatedBy).HasMaxLength(200);
            builder.Property(g => g.UpdatedAt);

            // Define Table Name
            builder.ToTable("Groups");
        }
    }
}
