using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCoursePerSessionTimeRepository
{
    void Add(TeacherPerCoursePerSessionTime teacherPerCoursePerSessionTime);
    Task SaveChangesAsync();
}