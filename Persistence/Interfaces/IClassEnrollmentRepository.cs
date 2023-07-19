using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface IClassEnrollmentRepository
{
    void Add(ClassEnrollment classEnrollment);
    Task SaveChangesAsync();
}