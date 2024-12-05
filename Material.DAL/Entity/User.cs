namespace Material.DAL.Entity;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public AuthorizationInfo? AuthorizationInfo { get; set; }
    
    public IEnumerable<MaterialEntity> Materials { get; set; }
}