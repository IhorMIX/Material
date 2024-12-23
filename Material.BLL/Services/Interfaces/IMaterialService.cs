using Material.BLL.Models;

namespace Material.BLL.Services.Interfaces;

public interface IMaterialService : IBaseService<MaterialEntityModel>
{
    Task CreateMaterialAsync(int userId, MaterialEntityModel material, CancellationToken cancellationToken = default);

    Task DeleteMaterialAsync(int userId, int materialId, CancellationToken cancellationToken = default);
    Task AddMaterialToFavoritesAsync(int userId, int materialId,
        CancellationToken cancellationToken = default);
    
    Task RemoveMaterialFromFavoritesAsync(int userId, int materialId, CancellationToken cancellationToken = default);

    Task<MaterialEntityModel> UpdateMaterialAsync(int id, MaterialEntityModel material, CancellationToken cancellationToken = default);

    Task<MaterialEntityModel?> GetMaterialByName(string name, CancellationToken cancellationToken = default);

    Task<IEnumerable<MaterialEntityModel>> GetMaterialsFromFavoriteListAsync(int userId,
        CancellationToken cancellationToken = default);
}