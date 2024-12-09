namespace Material.DAL.Entity;

public class FavoriteList: BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public ICollection<FavoriteMaterial> FavoriteMaterials { get; set; } = new List<FavoriteMaterial>();
}