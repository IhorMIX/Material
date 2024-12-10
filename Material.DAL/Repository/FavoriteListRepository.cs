using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.DAL.Repository;

public class FavoriteListRepository(MaterialDbContext materialDbContext) : IFavoriteListRepository
{
    private readonly MaterialDbContext _materialDbContext = materialDbContext;
    public IQueryable<FavoriteList> GetAll()
    {
        return _materialDbContext.FavoriteLists
            .Include(i => i.User)
            .Include(i => i.Materials)
            .AsQueryable();
    }
    public async Task<FavoriteList?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _materialDbContext.FavoriteLists
            .Include(c => c.User)
            .Include(c => c.Materials)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task RemoveMaterialAsync(MaterialEntity material, int userId, CancellationToken cancellationToken = default)
    {
        var favoriteList = await _materialDbContext.FavoriteLists
            .Include(fl => fl.Materials)
            .FirstOrDefaultAsync(fl => fl.UserId == userId, cancellationToken);

        favoriteList!.Materials.Remove(material);

        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }


    public async Task AddMaterialAsync(MaterialEntity material,User user, CancellationToken cancellationToken = default)
    {
        var addMaterial = new FavoriteList()
        {
            Materials = new List<MaterialEntity> { material },
            User = user
        };
        _materialDbContext.FavoriteLists.Add(addMaterial);
        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }
}