namespace Material.BLL.Models;

public class FavoriteListMaterialModel : BaseModel
{
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;

    public int MaterialId { get; set; }
    public MaterialEntityModel Material { get; set; } = null!;
}