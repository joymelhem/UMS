using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostgresContext _postgresContext;
    
    public UserRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public async Task<User> GetById(long id)
    {
        return await _postgresContext.Users.FindAsync(id);
    }
    

    public async Task SaveChangesAsync()
    {
        await _postgresContext.SaveChangesAsync();
    }
}