using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.DAL.Repository;

public class FavoriteListMaterialRepository(MaterialDbContext materialDbContext) : IFavoriteListMaterialRepository
{
    private readonly MaterialDbContext _materialDbContext = materialDbContext;

    public IQueryable<FavoriteListMaterial> GetAll()
    {
         return _materialDbContext.FavoriteListMaterials.AsQueryable();
    }

    public async Task<FavoriteListMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _materialDbContext.FavoriteListMaterials
            .AsNoTracking() 
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task AddMaterial(MaterialEntity material, User user, CancellationToken cancellationToken = default)
    {
        var favoriteListMaterial = new FavoriteListMaterial
        {
            Material = material, 
            User = user         
        };

        await _materialDbContext.FavoriteListMaterials.AddAsync(favoriteListMaterial, cancellationToken);
        await _materialDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveMaterial(int materialId, int userId, CancellationToken cancellationToken = default)
    {
        var favoriteListMaterial = await _materialDbContext.FavoriteListMaterials
            .FirstOrDefaultAsync(f => f.MaterialId == materialId && f.UserId == userId, cancellationToken);

        if (favoriteListMaterial != null)
        {
            _materialDbContext.FavoriteListMaterials.Remove(favoriteListMaterial);
            await _materialDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}