using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.Configurations;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);
        
        // CreationDate
        builder.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("NOW()");
        
        // ModificationDate
        builder.Property(x => x.ModificationDate)
            .IsRequired(false);
        
        // DeletionDate
        builder.Property(x => x.DeletionDate)
            .IsRequired(false);
    }
}
