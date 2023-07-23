using System.Reflection;
using App.Handlers;
using Persistence.Repositories;
using DomainLibrary.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(options=> options.Select().Filter().OrderBy());
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ISessionTimeRepository, SessionTimeRepository>();
builder.Services.AddTransient<IClassEnrollmentRepository, ClassEnrollmentRepository>();
builder.Services.AddTransient<ITeacherPerCourseRepository, TeacherPerCourseRepository>();
builder.Services.AddTransient<ITeacherPerCoursePerSessionTimeRepository, TeacherPerCoursePerSessionTimeRepository>();
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
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/my-project-id",
            ValidateAudience = true,
            ValidAudience = "ums-31836",
            ValidateLifetime = true
        };
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
app.UseAuthorization();

app.MapControllers();

app.Run();