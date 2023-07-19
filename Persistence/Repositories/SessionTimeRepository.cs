using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using Persistence.Models;

namespace Persistence.Repositories;

public class SessionTimeRepository : ISessionTimeRepository
{
    private readonly PostgresContext _postgresContext;
    
    public SessionTimeRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }
    public async Task<SessionTime> GetById(long id)
    {
        return await _postgresContext.SessionTimes.FindAsync(id);
    }

    public void Add(SessionTime sessionTime)
    {
        _postgresContext.SessionTimes.Add(sessionTime);
    }
    
    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
}