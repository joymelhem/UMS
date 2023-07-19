using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using Persistence.Models;

namespace Persistence.Repositories;

public class ClassEnrollmentRepository : IClassEnrollmentRepository
{
    private readonly PostgresContext _postgresContext;
    
    public ClassEnrollmentRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public void Add(ClassEnrollment classEnrollment)
    {
        _postgresContext.ClassEnrollments.Add(classEnrollment);
    }
    
    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
}