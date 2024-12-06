using Material.BLL.Models;

namespace Material.BLL.Services.Interfaces;

public interface IUserService : IBaseService<UserModel>
{
    Task CreateUserAsync(UserModel user, CancellationToken cancellationToken = default);

    Task DeleteUserAsync(int id, CancellationToken cancellationToken = default);

    Task<UserModel> UpdateUserAsync(int id, UserModel user, CancellationToken cancellationToken = default);
    
    Task<UserModel> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<UserModel?> GetUserByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken = default);
    Task AddAuthorizationValueAsync(UserModel user, string refreshToken, DateTime? expiredDate = null,
        CancellationToken cancellationToken = default);
    Task<UserModel?> GetUserByLogin(string login, CancellationToken cancellationToken = default);
    
    Task LogOutAsync(int userId, CancellationToken cancellationToken = default);
}