using AutoMapper;
using Material.BLL.Exceptions;
using Material.BLL.Models;
using Material.BLL.Services.Interfaces;
using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Material.BLL.Helpers;
using ReflectionHelper = Material.BLL.Helpers.ReflectionHelper;

namespace Material.BLL.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<UserModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {id} not found");
        
        var userModel = _mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task CreateUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetAll()
            .FirstOrDefaultAsync(i => i.Login == user.Login,
                cancellationToken);
        
        if (userDb is not null && userDb.Login == user.Login)
            throw new AlreadyLoginAndEmailException("Login is already used by another user");
        
        var userDbModel = _mapper.Map<User>(user);
        userDbModel.Password = PasswordHelper.HashPassword(userDbModel.Password);
        await _userRepository.CreateUser(userDbModel, cancellationToken);
        user.Id = userDbModel.Id;
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {id} not found");

        await _userRepository.DeleteUserAsync(userDb, cancellationToken);
    }

    public async Task<UserModel> UpdateUserAsync(int id, UserModel user, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {id} not found");
        
        userDb!.Password = string.IsNullOrEmpty(user.Password)
            ? userDb.Password
            : PasswordHelper.HashPassword(user.Password);
        
        foreach (var propertyMap in ReflectionHelper.WidgetUtil<UserModel, User>.PropertyMap)
        {
            var userProperty = propertyMap.Item1;
            var userDbProperty = propertyMap.Item2;
        
            var userSourceValue = userProperty.GetValue(user);
            var userTargetValue = userDbProperty.GetValue(userDb);
        
            if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
            {
                userDbProperty.SetValue(userDb, userSourceValue);
            }
        }
        
        await _userRepository.UpdateUserAsync(userDb, cancellationToken);
        var userModel = _mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task<UserModel> GetUserByRefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetAll()
            .FirstOrDefaultAsync(i =>
                    i.AuthorizationInfo != null &&
                    i.AuthorizationInfo.RefreshToken == refreshToken,
                cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this refresh token {refreshToken} not found");

        if (userDb.AuthorizationInfo is not null && userDb.AuthorizationInfo.ExpiredDate <= DateTime.Now.AddDays(-1))
            throw new TimeoutException();

        var userModel = _mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task<UserModel?> GetUserByLoginAndPasswordAsync(string login, string password,
        CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetAll().FirstOrDefaultAsync(i => i.Login == login, cancellationToken);
        if (userDb is null)
            throw new WrongLoginOrPasswordException("Wrong login or password");

        if (!PasswordHelper.VerifyHashedPassword(userDb!.Password, password))
        {
            throw new WrongLoginOrPasswordException("Wrong login or password");
        }

        var userModel = _mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task AddAuthorizationValueAsync(UserModel user, string refreshToken,
        DateTime? expiredDate = null,
        CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(user.Id, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {user.Id} not found");

        if (userDb!.AuthorizationInfo is not null && userDb.AuthorizationInfo.ExpiredDate <= DateTime.Now.AddDays(-1))
            await LogOutAsync(user.Id, cancellationToken);

        userDb.AuthorizationInfo = new AuthorizationInfo
        {
            RefreshToken = refreshToken,
            ExpiredDate = expiredDate
        };
        await _userRepository.UpdateUserAsync(userDb, cancellationToken);
    }

    public async Task<UserModel?> GetUserByLogin(string login, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetAll()
            .FirstOrDefaultAsync(i => i.Login == login, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this login {login} not found");

        var userModel = _mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task LogOutAsync(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {userId} not found");

        if (userDb.AuthorizationInfo is not null)
        {
            userDb.AuthorizationInfo = null;
            await _userRepository.UpdateUserAsync(userDb, cancellationToken);
        }
        else throw new NullReferenceException($"User with this token not found");
    }

}