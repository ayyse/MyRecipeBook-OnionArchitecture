using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        // Table name
        builder.ToTable("Users");
        
        // Email
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(25)
            .HasComment("Email");
        
        // Password
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasComment("PasswordHash");
    }
}