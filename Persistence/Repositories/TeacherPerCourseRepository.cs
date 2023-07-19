using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Repositories;

public class TeacherPerCourseRepository : ITeacherPerCourseRepository
{
    private readonly PostgresContext _postgresContext;
    
    public TeacherPerCourseRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }
    public async Task<TeacherPerCourse> GetById(long id)
    {
        return await _postgresContext.TeacherPerCourses.FindAsync(id);
    }

    public void Add(TeacherPerCourse teacherPerCourse)
    {
        _postgresContext.TeacherPerCourses.Add(teacherPerCourse);
    }
    
    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
}