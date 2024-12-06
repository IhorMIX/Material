namespace Material.BLL.Services.Interfaces;

public interface IBaseService<TModel> where TModel : class
{
    Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}