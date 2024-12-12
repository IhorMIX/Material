using AutoMapper;
using Material.BLL.Exceptions;
using Material.BLL.Helpers;
using Material.BLL.Models;
using Material.BLL.Services.Interfaces;
using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.BLL.Services;

public class MaterialService(IMaterialRepository materialRepository, IMapper mapper,IFavoriteListMaterialRepository favoriteListMaterialRepository
,IUserRepository userRepository) : IMaterialService
{
    private readonly IMaterialRepository _materialRepository = materialRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IFavoriteListMaterialRepository _favoriteListMaterialRepository = favoriteListMaterialRepository;
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<MaterialEntityModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(id, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"User with this Id {id} not found");
        
        var materialModel = _mapper.Map<MaterialEntityModel>(materialDb);
        return materialModel;
    }

    public async Task CreateMaterialAsync(int userId, MaterialEntityModel material,  CancellationToken cancellationToken = default)
    {
        var userDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {userId} not found");
        
        if (userDb.Login is not "admin")
            throw new NoRightException("You don't have permissions");
        
        var isMaterialExists = await _materialRepository.GetAll()
            .AnyAsync(i => i.Name == material.Name, cancellationToken);

        if (isMaterialExists)
            throw new AlreadyMaterialNameException("Material name is already used by another user");

        var materialDbModel = _mapper.Map<MaterialEntity>(material);
        await _materialRepository.CreateMaterial(materialDbModel, cancellationToken);
        material.Name = materialDbModel.Name;
    }

    public async Task DeleteMaterialAsync(int userId, int materialId,  CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(materialId, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this Id {materialId} not found");
        
        var userDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {userId} not found");
        
        if (userDb.Login is not "admin")
            throw new NoRightException("You don't have permissions");
        
        await _materialRepository.DeleteMaterialAsync(materialDb, cancellationToken);
    }

    public async Task AddMaterialToFavoritesAsync(int userId, int materialId, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(materialId, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this Id {materialId} not found");
        
        var userDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {userId} not found");
        
        var isAlreadyInFavorites = await _favoriteListMaterialRepository.GetAll()
            .AnyAsync(f => f.UserId == userId && f.MaterialId == materialId, cancellationToken);

        if (isAlreadyInFavorites)
            throw new MaterialAlreadyInFavoritesException($"Material with Id {materialId} is already in the favorites list for user with Id {userId}");

        await _favoriteListMaterialRepository.AddMaterial(materialDb, userDb, cancellationToken);
    }
    
    public async Task RemoveMaterialFromFavoritesAsync(int userId, int materialId, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(materialId, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this Id {materialId} not found");
        
        var userDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new UserNotFoundException($"User with this Id {userId} not found");
        
        var isInFavorites = await _favoriteListMaterialRepository.GetAll()
            .AnyAsync(f => f.UserId == userId && f.MaterialId == materialId, cancellationToken);

        if (!isInFavorites)
            throw new MaterialNotInFavoritesException($"Material with Id {materialId} is not in the favorites list for user with Id {userId}");
        
        await _favoriteListMaterialRepository.RemoveMaterial(materialDb.Id, userDb.Id, cancellationToken);
    }


    public async Task<MaterialEntityModel> UpdateMaterialAsync(int materialId, MaterialEntityModel materialEntityModel, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(materialId, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this Id {materialId} not found");
        
        foreach (var propertyMap in ReflectionHelper.WidgetUtil<MaterialEntityModel, MaterialEntity>.PropertyMap)
        {
            var userProperty = propertyMap.Item1;
            var userDbProperty = propertyMap.Item2;
        
            var userSourceValue = userProperty.GetValue(materialEntityModel);
            var userTargetValue = userDbProperty.GetValue(materialDb);
        
            if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
            {
                userDbProperty.SetValue(materialDb, userSourceValue);
            }
        }
        
        await _materialRepository.UpdateMaterialAsync(materialDb, cancellationToken);
        var materialModel = _mapper.Map<MaterialEntityModel>(materialDb);
        return materialModel;
    }

    public async Task<MaterialEntityModel?> GetMaterialByName(string name, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetAll()
            .FirstOrDefaultAsync(i => i.Name == name, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this name {name} not found");

        var materialModel = _mapper.Map<MaterialEntityModel>(materialDb);
        return materialModel;
    }

    
}