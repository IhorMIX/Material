namespace Material.BLL.Models;

public class UserModel : BaseModel
{  
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public AuthorizationInfoModel? AuthorizationInfo { get; set; }
    
    public IEnumerable<MaterialEntityModel> Materials { get; set; }
}