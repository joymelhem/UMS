using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ISessionTimeRepository
{
    void Add(SessionTime sessionTime);
    Task SaveChangesAsync();
}