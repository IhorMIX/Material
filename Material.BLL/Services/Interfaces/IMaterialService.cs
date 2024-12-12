using Material.BLL.Models;

namespace Material.BLL.Services.Interfaces;

public interface IMaterialService : IBaseService<MaterialEntityModel>
{
    Task CreateMaterialAsync(MaterialEntityModel material, CancellationToken cancellationToken = default);

    Task DeleteMaterialAsync(int id, CancellationToken cancellationToken = default);

    // Task RemoveMaterialFromFavoritesAsync(string materialName, int userId,
    //     CancellationToken cancellationToken = default);

    Task AddMaterialToFavoritesAsync(int userId, int materialId,
        CancellationToken cancellationToken = default);

    Task<MaterialEntityModel> UpdateMaterialAsync(int id, MaterialEntityModel material, CancellationToken cancellationToken = default);

    Task<MaterialEntityModel?> GetMaterialByName(string name, CancellationToken cancellationToken = default);
    
}