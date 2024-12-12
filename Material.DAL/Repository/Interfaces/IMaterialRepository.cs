using Material.DAL.Entity;

namespace Material.DAL.Repository.Interfaces;

public interface IMaterialRepository : IBasicRepository<MaterialEntity>
{
    Task CreateMaterial(MaterialEntity material, CancellationToken cancellationToken = default);

    Task<MaterialEntity?> FindMaterialAsync(int id, CancellationToken cancellationToken = default);

    Task UpdateMaterialAsync(MaterialEntity material, CancellationToken cancellationToken = default);

    Task DeleteMaterialAsync(MaterialEntity material, CancellationToken cancellationToken = default);
}