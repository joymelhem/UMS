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

 
    public async Task<List<Course>> Delete(long id)
    {
        var student = _postgresContext.Courses.Find(id);

        if (student != null)
        {
            _postgresContext.Courses.Remove(student);
            await _postgresContext.SaveChangesAsync();
        }

        var updatedStudents = await _postgresContext.Courses.ToListAsync();
        return updatedStudents;
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