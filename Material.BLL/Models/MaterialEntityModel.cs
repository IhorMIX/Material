namespace Material.BLL.Models;

public class MaterialEntityModel : BaseModel
{
    public string Name { get; set; }
    public int UserId { get; set; }

    public string TestNameParam { get; set; } = null!;
}