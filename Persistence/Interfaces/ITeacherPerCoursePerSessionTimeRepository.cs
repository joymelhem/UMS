using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCoursePerSessionTimeRepository
{
    Task<TeacherPerCoursePerSessionTime> GetById(long id);

    void Add(TeacherPerCoursePerSessionTime teacherPerCoursePerSessionTime);
    Task SaveChangesAsync();
}