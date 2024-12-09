namespace Material.DAL.Entity;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public AuthorizationInfo? AuthorizationInfo { get; set; }
    
    public FavoriteList? FavoriteList { get; set; }
    public ICollection<MaterialEntity> Materials { get; set; } = new List<MaterialEntity>();
}