using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface IClassEnrollmentRepository
{
    ClassEnrollment GetById(long id);
    ClassEnrollment GetByName(string name);
    List<ClassEnrollment> Delete(long id);
    Task<List<ClassEnrollment>> GetAll();
    ClassEnrollment Update(ClassEnrollment classEnrollment);
    ClassEnrollment Add(ClassEnrollment classEnrollment);
}