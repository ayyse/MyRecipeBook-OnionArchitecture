using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.Configurations;

public class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        // Table name
        builder.ToTable("Categories");
        
        // Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        // Parent - Child relationship (Self Referencing)
        builder.HasOne(x => x.ParentCategory)
            .WithMany(x => x.SubCategories)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Restrict: Alt kategoriler varken üst kategoriyi silemesin


        // Relationships: Category -> Recipes (1 - many)
        builder.HasMany(x => x.Recipes)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade: Eğer bir parent (ana) kayıt silinirse, ona bağlı child (alt) kayıtların otomatik olarak silinmesini sağlar
    }
}