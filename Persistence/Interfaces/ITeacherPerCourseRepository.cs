using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCourseRepository
{
    TeacherPerCourse GetById(long id);
    TeacherPerCourse GetByName(string name);
    List<TeacherPerCourse> Delete(long id);
    Task<List<TeacherPerCourse>> GetAll();
    TeacherPerCourse Update(TeacherPerCourse teacherPerCourse);
    TeacherPerCourse Add(TeacherPerCourse teacherPerCourse);
}