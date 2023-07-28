using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ICourseRepository
{
    Course GetById(long id);
    Task<List<Course>> Delete(long id);
    Task<List<Course>> GetAll();
    void Add(Course course);
    Task SaveChangesAsync();

}