using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface IUserRepository
{
    User GetById(long id);
    User GetByName(string name);
    List<User> Delete(long id);
    Task<List<User>> GetAll();
    User Update(User user);
    User Add(User user);
}