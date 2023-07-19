using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using Persistence.Models;

namespace Persistence.Repositories;

public class TeacherPerCoursePerSessionTimeRepository : ITeacherPerCoursePerSessionTimeRepository
{
    private readonly PostgresContext _postgresContext;
    
    public TeacherPerCoursePerSessionTimeRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }
    public async Task<TeacherPerCoursePerSessionTime> GetById(long id)
    {
        return await _postgresContext.TeacherPerCoursePerSessionTimes.FindAsync(id);
    }

    public void Add(TeacherPerCoursePerSessionTime teacherPerCoursePerSessionTime)
    {
        _postgresContext.TeacherPerCoursePerSessionTimes.Add(teacherPerCoursePerSessionTime);
    }
    
    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
}