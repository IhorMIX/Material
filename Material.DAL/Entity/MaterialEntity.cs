namespace Material.DAL.Entity;

public class MaterialEntity : BaseEntity
{
    public string Name { get; set; }
    public string TestNameParam { get; set; } = null!;
    
    public ICollection<FavoriteList> FavoriteLists { get; set; } = new List<FavoriteList>();
}