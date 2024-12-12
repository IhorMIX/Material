namespace Material.BLL.Models;

public class MaterialEntityModel : BaseModel
{
    public string Name { get; set; }

    public string TestNameParam { get; set; } = null!;

    public IEnumerable<FavoriteListMaterialModel> FavoriteListMaterial { get; set; } = null!;
}