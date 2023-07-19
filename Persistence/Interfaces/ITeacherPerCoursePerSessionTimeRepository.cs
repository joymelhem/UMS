using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ITeacherPerCoursePerSessionTimeRepository
{
    TeacherPerCoursePerSessionTime GetById(long id);
    TeacherPerCoursePerSessionTime GetByName(string name);
    List<TeacherPerCoursePerSessionTime> Delete(long id);
    Task<List<TeacherPerCoursePerSessionTime>> GetAll();
    TeacherPerCoursePerSessionTime Update(TeacherPerCoursePerSessionTime teacherPerCoursePerSessionTime);
    TeacherPerCoursePerSessionTime Add(TeacherPerCoursePerSessionTime teacherPerCoursePerSessionTime);
}