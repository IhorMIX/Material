using AutoMapper;
using Material.BLL.Exceptions;
using Material.BLL.Helpers;
using Material.BLL.Models;
using Material.BLL.Services.Interfaces;
using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.BLL.Services;

public class MaterialService(IMaterialRepository materialRepository, IMapper mapper) : IMaterialService
{
    private readonly IMaterialRepository _materialRepository = materialRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<MaterialEntityModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(id, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"User with this Id {id} not found");
        
        var materialModel = _mapper.Map<MaterialEntityModel>(materialDb);
        return materialModel;
    }

    public async Task CreateMaterialAsync(MaterialEntityModel material, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetAll()
            .FirstOrDefaultAsync(i => i.Name == material.Name,
                cancellationToken);
        
        if (materialDb is not null && materialDb.Name == material.Name)
            throw new AlreadyMaterialNameException("Material name is already used by another user");
        
        var materialDbModel = _mapper.Map<MaterialEntity>(material);
        await _materialRepository.CreateMaterial(materialDbModel, cancellationToken);
        material.Name = materialDbModel.Name;
    }

    public async Task DeleteMaterialAsync(int id, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(id, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this Id {id} not found");

        await _materialRepository.DeleteMaterialAsync(materialDb, cancellationToken);
    }

    public async Task<MaterialEntityModel> UpdateMaterialAsync(int id, MaterialEntityModel material, CancellationToken cancellationToken = default)
    {
        var materialDb = await _materialRepository.GetByIdAsync(id, cancellationToken);
        if (materialDb is null)
            throw new MaterialNotFoundException($"Material with this Id {id} not found");
        
        foreach (var propertyMap in ReflectionHelper.WidgetUtil<MaterialEntityModel, MaterialEntity>.PropertyMap)
        {
            var userProperty = propertyMap.Item1;
            var userDbProperty = propertyMap.Item2;
        
            var userSourceValue = userProperty.GetValue(material);
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