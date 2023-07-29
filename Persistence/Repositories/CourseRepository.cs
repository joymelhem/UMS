using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly PostgresContext _postgresContext;
    
    public CourseRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public Course GetById(long id)
    {
        return _postgresContext.Courses.Find(id);
    }
    
    public void Delete(Course course)
    {
        _postgresContext.Courses.Remove(course);
    }

    public async Task<List<Course>> GetAll()
    {
        return _postgresContext.Courses.ToList();
    }

    public void Add(Course course)
    {
        _postgresContext.Courses.Add(course);
    }
    
    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
    
    
}