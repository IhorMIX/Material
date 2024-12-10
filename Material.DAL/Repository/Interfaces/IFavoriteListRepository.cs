using Material.DAL.Entity;

namespace Material.DAL.Repository.Interfaces;

public interface IFavoriteListRepository : IBasicRepository<FavoriteList>
{
    Task RemoveMaterialAsync(MaterialEntity material,int userId, CancellationToken cancellationToken = default);
    
    Task AddMaterialAsync(MaterialEntity material,User user, CancellationToken cancellationToken = default);
}