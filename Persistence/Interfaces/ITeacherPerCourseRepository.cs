using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCourseRepository
{
    void Add(TeacherPerCourse teacherPerCourse);
    Task SaveChangesAsync();
}