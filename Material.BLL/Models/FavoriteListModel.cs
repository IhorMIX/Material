namespace Material.BLL.Models;

public class FavoriteListModel : BaseModel
{
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public ICollection<MaterialEntityModel> Materials { get; set; } = new List<MaterialEntityModel>();
}