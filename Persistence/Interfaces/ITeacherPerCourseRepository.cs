using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCourseRepository
{
    Task<TeacherPerCourse> GetById(long id);
    Task<Course> GetByCourseId(long id);
    Task<List<User>> GetStudentsByTeacherId(long teacherId);
    void Add(TeacherPerCourse teacherPerCourse);
    Task SaveChangesAsync();
    Task<List<string>> GetCommonStudents(long teacherId1, long teacherId2);
}