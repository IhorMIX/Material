using Material.DAL.Entity;
using Material.DAL.Repository.Interfaces;

namespace Material.DAL.Repository;

public class MaterialRepository : IMaterialRepository
{
    public IQueryable<MaterialEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<MaterialEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}