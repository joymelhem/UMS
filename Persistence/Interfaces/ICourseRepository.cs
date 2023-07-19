using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ICourseRepository
{
    Course GetById(long id);
    Course GetByName(string name);
    Task<List<Course>> Delete(long id);
    Task<List<Course>> GetAll();
    Course Update(Course course);
    void Add(Course course);
    Task SaveChangesAsync();

}