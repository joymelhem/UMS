using System.Reflection;
using App.Handlers;
using Persistence.Repositories;
using DomainLibrary.Interfaces;
using MediatR;
using Persistence.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ISessionTimeRepository, SessionTimeRepository>();
builder.Services.AddTransient<IClassEnrollmentRepository, ClassEnrollmentRepository>();
builder.Services.AddTransient<ITeacherPerCourseRepository, TeacherPerCourseRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetByIdQueryHandler>(
));
builder.Services.AddDbContext<PostgresContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();