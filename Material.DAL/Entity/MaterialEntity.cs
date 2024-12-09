namespace Material.DAL.Entity;

public class MaterialEntity : BaseEntity
{
    public string Name { get; set; }
    public string TestNameParam { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public ICollection<FavoriteMaterial> FavoriteMaterials { get; set; } = new List<FavoriteMaterial>();
}