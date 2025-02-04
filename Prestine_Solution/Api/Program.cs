using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Application.Utils;
using Azure.Identity;
using Domain.Interfaces;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to handle Firebase credentials
builder.Services.AddSingleton<IFirebaseCredentialProvider>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var environment = sp.GetRequiredService<IHostEnvironment>();

    return new FirebaseCredentialProvider(configuration, environment);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    // Add JWT token support to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert Access Token with 'Bearer ' prefix, Ex: 'Bearer ABCXYZ'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


// Register the DbContext
builder.Services.AddDbContext<SkincareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
var key = Encoding.ASCII.GetBytes(
    builder.Configuration["Jwt:SecretKey"] ??
    throw new InvalidOperationException("JWT Secret Key is not configured"));

// Add Google authentication along with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})// Add to existing authentication configuration
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"];
    options.ClientSecret = builder.Configuration["Google:ClientSecret"];
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RestrictPolicy", policy =>
        policy.RequireRole("Manager", "Admin"));
    options.AddPolicy("StandardPolicy", policy =>
        policy.RequireRole("Staff", "Therapist", "Manager", "Admin"));
    options.AddPolicy("OpenPolicy", policy =>
        policy.RequireRole("Customer", "Staff", "Therapist", "Manager", "Admin"));
});

// Register DI for services such as Repositories, Application Services, etc
builder.Services.AddScoped<DbContext, SkincareDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthService, AuthService>();


    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITherapistRepository, TherapistRepository>();
    builder.Services.AddScoped<ITherapistProfileRepository, TherapistProfileRepository>();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITherapistService, TherapistService>();
    builder.Services.AddScoped<ITherapistProfileService, TherapistProfileService>();

    builder.Services.AddScoped<IImageService, FirebaseImageService>();

builder.Services.AddScoped<GoogleTokenValidator>();

// Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add CORS service and allow all origins for simplicity (you can restrict this to specific origins later)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow all origins
              .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any headers
    });
});

var app = builder.Build();

app.UseCors("AllowAll"); // Enable CORS policy globally

// Enable Swagger UI for both development and production environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty; // Opens Swagger at the root URL
});

if (!app.Environment.IsDevelopment())
{
    // Enforce HTTPS for production
    app.UseHttpsRedirection();

}

// Configure the HTTP request pipeline
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
