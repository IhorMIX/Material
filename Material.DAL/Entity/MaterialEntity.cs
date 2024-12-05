namespace Material.DAL.Entity;

public class MaterialEntity : BaseEntity
{
    public string Name { get; set; }
    public int UserId { get; set; }

    public string TestNameParam { get; set; } = null!;
}