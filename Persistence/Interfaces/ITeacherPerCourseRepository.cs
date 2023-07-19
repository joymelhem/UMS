using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCourseRepository
{
    Task<TeacherPerCourse> GetById(long id);
    void Add(TeacherPerCourse teacherPerCourse);
    Task SaveChangesAsync();
}