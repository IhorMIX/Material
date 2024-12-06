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
        CreateMap<UserCreateViewModel, UserModel>();
        CreateMap<UserUpdateViewModel, UserModel>();
        CreateMap<UserModel, UserViewModel>();
    }
}
