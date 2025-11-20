using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Infrastructure.Configurations;

namespace MyRecipeBook.Infrastructure.DbContexts;

public class MyRecipeBookDbContext : DbContext
{
    public MyRecipeBookDbContext(DbContextOptions<MyRecipeBookDbContext> options)
        : base(options)
    {
    }
    
    // Entities
    public DbSet<Recipe> Recipes => Set<Recipe>(); // null reference problemi olmaz. Her zaman Set<Recipe>() metodunu çağırarak çalışır, EF tarafından runtime’da set edilmeye gerek yok.
    public DbSet<User> Users => Set<User>(); 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configurations
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}