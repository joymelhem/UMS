using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface IUserRepository
{
    Task<User> GetById(long id);
    Task<User> GetByEmail(string email);
    Task SaveChangesAsync();
}