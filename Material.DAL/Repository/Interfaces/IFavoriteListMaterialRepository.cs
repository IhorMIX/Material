using Material.DAL.Entity;

namespace Material.DAL.Repository.Interfaces;

public interface IFavoriteListMaterialRepository : IBasicRepository<FavoriteListMaterial>
{
    Task AddMaterial(MaterialEntity material, User user,CancellationToken cancellationToken = default);
    
    Task RemoveMaterial(int materialId,int userId, CancellationToken cancellationToken = default);
}