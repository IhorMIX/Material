namespace Material.DAL.Entity;

public class FavoriteListMaterial : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MaterialId { get; set; }
    public MaterialEntity Material { get; set; } = null!;
}