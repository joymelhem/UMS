using System.Reflection;
using System.Security.Claims;
using System.Text;
using App.Handlers;
using Persistence.Repositories;
using DomainLibrary.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Models;
using Persistence.Services;
using UMS.Api.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(options=> options.Select().Filter().OrderBy());
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ISessionTimeRepository, SessionTimeRepository>();
builder.Services.AddTransient<IClassEnrollmentRepository, ClassEnrollmentRepository>();
builder.Services.AddTransient<ITeacherPerCourseRepository, TeacherPerCourseRepository>();
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddTransient<ITeacherPerCoursePerSessionTimeRepository, TeacherPerCoursePerSessionTimeRepository>();
builder.Services.AddSingleton<FirebaseMailService>(provider =>
{
    var firebaseAdminSdkJsonPath = "firebaseconfig.json";
    return new FirebaseMailService(firebaseAdminSdkJsonPath);
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetByIdQueryHandler>(
));
builder.Services.AddHttpClient("FirebaseClient", c =>
{
    c.BaseAddress = new Uri("https://identitytoolkit.googleapis.com/v1/accounts");
});
builder.Services.AddDbContext<PostgresContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UMS", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
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
builder.Services.AddLogging();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/ums-31836";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("wVeMNEEqCZVo7OZdByT8eIO34PvEqfXyHWJe3lVWusg="))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "1"); 
    });

    options.AddPolicy("TeacherOnly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "2"); 
    });

    options.AddPolicy("StudentOnly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "3"); 
    });
});
builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<TenantMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();