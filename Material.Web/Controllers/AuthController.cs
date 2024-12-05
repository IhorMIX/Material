using Material.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Material.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    // private readonly TokenHelper _tokenHelper;
}
