using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbContext
builder.Services.AddDbContext<SkincareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DI for services such as Repositories, Application Services, etc

// House DI
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IHouseService, HouseService>();

// User DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll"); // This enables CORS with the defined policy

// Enable Swagger UI for both development and production environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});

if (!app.Environment.IsDevelopment())
{
    // Enforce HTTPS for production
    app.UseHttpsRedirection();
}

// Configure the HTTP request pipeline
app.UseAuthorization();

app.MapControllers();

app.Run();
