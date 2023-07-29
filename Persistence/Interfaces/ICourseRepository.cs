using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ICourseRepository
{
    Course GetById(long id);
    void Delete(Course course);
    Task<List<Course>> GetAll();
    void Add(Course course);
    Task SaveChangesAsync();

}