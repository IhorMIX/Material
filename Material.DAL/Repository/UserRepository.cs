using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.DAL.Repository;

public class UserRepository(MaterialDbContext materialDbContext) : IUserRepository
{
    private readonly MaterialDbContext _materialDbContext = materialDbContext;
    
    public IQueryable<User> GetAll()
    {
        return _materialDbContext.Users.Include(i => i.AuthorizationInfo).AsQueryable();
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _materialDbContext.Users.Include(i => i.AuthorizationInfo)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public Task<List<User>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        return _materialDbContext.Users.Where(u => ids.Contains(u.Id)).ToListAsync(cancellationToken);
    }

    public async Task CreateUser(User user, CancellationToken cancellationToken = default)
    {
        await _materialDbContext.Users.AddAsync(user, cancellationToken);
        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> FindUserAsync(string login, CancellationToken cancellationToken = default)
    {
        return await _materialDbContext.Users
            .Include(i => i.AuthorizationInfo)
            .FirstOrDefaultAsync(i => i.Login == login, cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _materialDbContext.Users.Update(user);

        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _materialDbContext.Users.Remove(user);

        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }
}