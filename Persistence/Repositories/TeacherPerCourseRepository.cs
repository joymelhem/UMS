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
    public async Task<Course> GetByCourseId(long id)
    {
        return await _postgresContext.Courses.FindAsync(id);
    }

    public void Add(TeacherPerCourse teacherPerCourse)
    {
        _postgresContext.TeacherPerCourses.Add(teacherPerCourse);
    }
    
    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
    public async Task<List<User>> GetStudentsByTeacherId(long teacherId)
    {
        var students = await _postgresContext.Users
            .Where(u => u.RoleId == 3) 
            .Where(u => u.TeacherPerCourses.Any(tpc => tpc.TeacherId == teacherId))
            .ToListAsync();
        return students;
    }
    
    public async Task<List<string>> GetCommonStudents(long teacherId1, long teacherId2)
    {
        var commonStudents = await _postgresContext.ClassEnrollments
            .Include(classEnrollments => classEnrollments.Class)
            .Where(ce => ce.Class.TeacherId == teacherId1 || ce.Class.TeacherId == teacherId2)
            .Select(x => x.Student.Name)
            .Distinct()
            .ToListAsync();
        return commonStudents;
    }
}