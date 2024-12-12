using AutoMapper;
using Material.BLL.Models;
using Material.DAL.Entity;
using Material.Web.Models;

namespace Material.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
{
    public MapperModelsConfig()
    {
        CreateMap<UserModel, User>().ReverseMap();
        CreateMap<MaterialEntityModel, MaterialEntity>().ReverseMap();
        CreateMap<AuthorizationInfo, AuthorizationInfoModel>().ReverseMap();
        
        CreateMap<UserCreateViewModel, UserModel>();
        CreateMap<MaterialCreateViewModel, MaterialEntityModel>();
        CreateMap<MaterialAddViewModel, MaterialEntityModel>();
        CreateMap<UserUpdateViewModel, UserModel>();
        
        CreateMap<UserModel, UserViewModel>();
        CreateMap<MaterialEntityModel, MaterialViewModel>();
    }
}
