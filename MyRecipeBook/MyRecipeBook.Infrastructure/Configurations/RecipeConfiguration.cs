using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.Configurations;

public class RecipeConfiguration : BaseEntityConfiguration<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        // Base konfigürasyonu uygula
        base.Configure(builder);
        
        // Table name
        builder.ToTable("Recipes");
        
        // Name
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Recipe name");
        
        // Description
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(2000)
            .HasComment("Recipe description");
        
        // PreparationTime
        builder.Property(x => x.PreparationTime)
            .IsRequired()
            .HasComment("Preparation time in minutes");
        
        // CookingTime
        builder.Property(x => x.CookingTime)
            .IsRequired()
            .HasComment("Cooking time in minutes");
        
        // NumberOfServings
        builder.Property(x => x.NumberOfServings)
            .IsRequired()
            .HasComment("Number of servings");
    }
}
