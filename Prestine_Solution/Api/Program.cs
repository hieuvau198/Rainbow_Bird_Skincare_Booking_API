using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Application.Utils;
using Azure.Identity;
using Domain.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>(); // Load user secrets

#region Config Firebase Storage
var firebaseServiceAccountJson = builder.Configuration.GetFirebaseServiceAccountJson(builder.Environment);
if (!string.IsNullOrEmpty(firebaseServiceAccountJson))
{
    try
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(firebaseServiceAccountJson)
            });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Firebase initialization error: {ex.Message}");
    }
}

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Add Sign-in UI for Swagger page
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

#endregion

#region Register DbContext

builder.Services.AddDbContext<SkincareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region JWT, GG, Auth Policy
// Configure JWT Authentication
var key = Encoding.ASCII.GetBytes(
    builder.Configuration["Jwt:SecretKey"] ??
    throw new InvalidOperationException("JWT Secret Key is not configured"));

// Setup Key for Auth GG, JWT
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

// Setup Auth Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RestrictPolicy", policy =>
        policy.RequireRole("Manager", "Admin"));
    options.AddPolicy("StandardPolicy", policy =>
        policy.RequireRole("Staff", "Therapist", "Manager", "Admin"));
    options.AddPolicy("OpenPolicy", policy =>
        policy.RequireRole("Customer", "Staff", "Therapist", "Manager", "Admin"));
});
#endregion

#region Register DI for services such as Repositories, Application Services, etc
builder.Services.AddScoped<DbContext, SkincareDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITherapistService, TherapistService>();
builder.Services.AddScoped<ITherapistProfileService, TherapistProfileService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IWorkingDayService, WorkingDayService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ITherapistAvailabilityService, TherapistAvailabilityService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ICustomerQuizService, CustomerQuizService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IQuizRecommendationService, QuizRecommendationService>();
builder.Services.AddScoped<ICustomerAnswerService, CustomerAnswerService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICancelBookingService, CancelBookingService>();
builder.Services.AddScoped<ICancelPolicyService, CancelPolicyService>();
builder.Services.AddScoped<IPaymentPolicyService, PaymentPolicyService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICustomerRatingService, CustomerRatingService>();
builder.Services.AddScoped<IFeedbackQuestionService, FeedbackQuestionService>();
builder.Services.AddScoped<IFeedbackAnswerService, FeedbackAnswerService>();
builder.Services.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();
builder.Services.AddScoped<ICustomerFeedbackAnswerService, CustomerFeedbackAnswerService>();
builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
builder.Services.AddScoped<IBlogCommentService, BlogCommentService>();
builder.Services.AddScoped<IHashtagService, HashtagService>();
builder.Services.AddScoped<IBlogNewsHashtagService,  BlogNewsHashtagService>();

builder.Services.AddScoped<IImageService, FirebaseImageService>();

builder.Services.AddScoped<GoogleTokenValidator>();
#endregion

#region Mapping DTOs with Entities
builder.Services.AddAutoMapper(
                               typeof(UserMappingProfile),
                               typeof(CustomerMappingProfile),
                               typeof(ManagerMappingProfile),
                               typeof(StaffMappingProfile),
                               typeof(TherapistMappingProfile),
                               typeof(TherapistProfileMappingProfile),
                               typeof(TherapistAvailabilityMappingProfile),
                               typeof(WorkingDayMappingProfile),
                               typeof(TimeSlotMappingProfile),
                               typeof(ServiceMappingProfile),
                               typeof(ServiceCategoryMappingProfile),
                               typeof(QuizMappingProfile),
                               typeof(QuestionMappingProfile),
                               typeof(CustomerQuizMappingProfile),
                               typeof(AnswerMappingProfile),
                               typeof(QuizRecommendationMappingProfile),
                               typeof(CustomerAnswerMappingProfile),
                               typeof(PaymentMappingProfile),
                               typeof(BookingMappingProfile),
                               typeof(CancelBookingMappingProfile),
                               typeof(CancelPolicyMappingProfile),
                               typeof(PaymentPolicyMappingProfile),
                               typeof(CustomerRatingMappingProfile),
                               typeof(FeedbackQuestionMappingProfile),
                               typeof(FeedbackAnswerMappingProfile),
                               typeof(CustomerFeedbackMappingProfile),
                               typeof(CustomerFeedbackAnswerMappingProfile),
                               typeof(BlogCommentMappingProfile));
#endregion

#region Add CORS service and allow all origins for simplicity
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow all origins
              .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any headers
    });
});
#endregion


var app = builder.Build();

app.UseCors("AllowAll"); // Enable CORS allowance

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
