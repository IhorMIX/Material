namespace Material.DAL.Entity;

public class FavoriteList: BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<MaterialEntity> Materials { get; set; } = new List<MaterialEntity>();
}