using ExpenseSharing.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        // Configure composite key
        builder.HasKey(ug => new { ug.UserId, ug.GroupId });

        // UserGroup to User relationship
        builder.HasOne(ug => ug.User)
               .WithMany(u => u.Groups)
               .HasForeignKey(ug => ug.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        // UserGroup to Group relationship
        builder.HasOne(ug => ug.Group)
               .WithMany(g => g.Members)
               .HasForeignKey(ug => ug.GroupId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("UserGroups");
    }
}
