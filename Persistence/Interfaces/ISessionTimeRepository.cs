using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface ISessionTimeRepository
{
    SessionTime GetById(long id);
    SessionTime GetByName(string name);
    List<SessionTime> Delete(long id);
    Task<List<SessionTime>> GetAll();
    SessionTime Update(SessionTime sessionTime);
    SessionTime Add(SessionTime sessionTime);
}