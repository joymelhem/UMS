using DomainLibrary.Entities;

namespace DomainLibrary.Interfaces;

public interface IRoleRepository
{
    Role GetById(long id);
    Role GetByName(string name);
    List<Role> Delete(long id);
    Task<List<Role>> GetAll();
    Role Update(Role role);
    Role Add(Role role);
}