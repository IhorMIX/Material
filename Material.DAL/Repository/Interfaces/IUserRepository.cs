using Material.DAL.Entity;

namespace Material.DAL.Repository.Interfaces;

public interface IUserRepository : IBasicRepository<User>
{
    Task<List<User>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default);
    
    Task CreateUser(User user, CancellationToken cancellationToken = default);

    Task<User?> FindUserAsync(string login, CancellationToken cancellationToken = default);

    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);

    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
}