using AutoMapper;
using Material.BLL.Exceptions;
using Material.BLL.Models;
using Material.BLL.Services.Interfaces;
using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.BLL.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<UserModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var userDb = await userRepository.GetByIdAsync(id, cancellationToken);
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
        // userDbModel.Password = PasswordHelper.HashPassword(userDbModel.Password);
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
        // var userDb = await _userRepository.GetByIdAsync(id, cancellationToken);
        //
        // _logger.LogAndThrowErrorIfNull(userDb, new UserNotFoundException($"User with this Id {id} not found"));
        //
        // userDb!.Password = string.IsNullOrEmpty(user.Password)
        //     ? userDb.Password
        //     : PasswordHelper.HashPassword(user.Password);
        //
        // foreach (var propertyMap in ReflectionHelper.WidgetUtil<ProfileModel, Profile>.PropertyMap)
        // {
        //     var userProperty = propertyMap.Item1;
        //     var userDbProperty = propertyMap.Item2;
        //
        //     var userSourceValue = userProperty.GetValue(user.Profile);
        //     var userTargetValue = userDbProperty.GetValue(userDb.Profile);
        //
        //     if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
        //     {
        //         userDbProperty.SetValue(userDb.Profile, userSourceValue);
        //     }
        // }
        //
        // await _userRepository.UpdateUserAsync(userDb, cancellationToken);
        // var userModel = _mapper.Map<UserModel>(userDb);
        // return userModel;
        throw new NotImplementedException();
    }

    public Task<UserModel> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetUserByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetUserByLogin(string login, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task LogOutAsync(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {userId} not found");

        if (userDb!.AuthorizationInfo is not null)
        {
            userDb.AuthorizationInfo = null;
            await _userRepository.UpdateUserAsync(userDb, cancellationToken);
        }
        else throw new NullReferenceException($"User with this token not found");
    }

}