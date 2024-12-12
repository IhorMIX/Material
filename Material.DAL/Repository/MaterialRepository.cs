using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.DAL.Repository;

public class MaterialRepository(MaterialDbContext materialDbContext) : IMaterialRepository
{
    private readonly MaterialDbContext _materialDbContext = materialDbContext;
    public IQueryable<MaterialEntity> GetAll()
    {
        return _materialDbContext.Materials.AsQueryable();
    }

    public async Task<MaterialEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _materialDbContext.Materials
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task CreateMaterial(MaterialEntity material, CancellationToken cancellationToken = default)
    {
        await _materialDbContext.Materials.AddAsync(material, cancellationToken);
        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<MaterialEntity?> FindMaterialAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _materialDbContext.Materials
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task UpdateMaterialAsync(MaterialEntity material, CancellationToken cancellationToken = default)
    {
        _materialDbContext.Materials.Update(material);
        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteMaterialAsync(MaterialEntity material, CancellationToken cancellationToken = default)
    {
        _materialDbContext.Materials.Remove(material);
        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }
}