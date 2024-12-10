namespace Material.BLL.Models;

public class MaterialEntityModel : BaseModel
{
    public string Name { get; set; }

    public string TestNameParam { get; set; } = null!;
    
    public ICollection<FavoriteListModel> FavoriteLists { get; set; } = new List<FavoriteListModel>();
}