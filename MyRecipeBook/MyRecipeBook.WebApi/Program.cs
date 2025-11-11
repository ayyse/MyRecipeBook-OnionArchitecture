using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Application.AppServices;
using MyRecipeBook.Application.Interfaces.AppServices;
using MyRecipeBook.Application.Interfaces.Repositories;
using MyRecipeBook.Infrastructure.DbContexts;
using MyRecipeBook.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL bağlantısı (Scoped => her request için yeni instance)
builder.Services.AddDbContext<MyRecipeBookDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Repositories (DbContext ile aynı ömür)
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

// AppServices (her HTTP request için yeni, Repository ile aynı yaşam süresi)
builder.Services.AddScoped<IRecipeAppService, RecipeAppService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();