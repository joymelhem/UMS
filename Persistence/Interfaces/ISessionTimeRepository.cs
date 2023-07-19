using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ISessionTimeRepository
{
    Task<SessionTime> GetById(long id);
    void Add(SessionTime sessionTime);
    Task SaveChangesAsync();
}