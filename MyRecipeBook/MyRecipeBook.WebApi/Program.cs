using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Application.AppServices;
using MyRecipeBook.Application.Interfaces.AppServices;
using MyRecipeBook.Application.Interfaces.Helpers;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Infrastructure.DbContexts;
using MyRecipeBook.Infrastructure.Helpers;
using MyRecipeBook.Infrastructure.Repositories;
using MyRecipeBook.Infrastructure.Settings;

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
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// appsettings.json içindeki konfigurasyonlar otomatik olarak sınıfa bind edilir
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// AppServices (her HTTP request için yeni, Repository ile aynı yaşam süresi)
builder.Services.AddScoped<IRecipeAppService, RecipeAppService>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();

// Helpers
builder.Services.AddScoped<ITokenHelper, JwtTokenHelper>();
builder.Services.AddScoped<IPasswordHelper, BcryptPasswordHelper>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // production'da true
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true, // token imzasını kontrol eder
            ValidateLifetime = true, // token süresini kontrol eder
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });

// CORS POLICY
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Angular
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true); // ÖNEMLİ: Chrome preflight bug fix
    });
});

builder.Services.AddControllers(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();