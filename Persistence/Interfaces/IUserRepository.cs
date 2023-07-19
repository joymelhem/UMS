using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface IUserRepository
{
    Task<User> GetById(long id);
    Task SaveChangesAsync();
}