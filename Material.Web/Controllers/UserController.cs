using AutoMapper;
using Material.BLL.Models;
using Material.BLL.Services.Interfaces;
using Material.Web.Extensions;
using Material.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Material.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
    : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateViewModel user,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to create user");

        await userService.CreateUserAsync(mapper.Map<UserModel>(user), cancellationToken);

        logger.LogInformation("User was created");

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateViewModel user,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to update user");

        var userId = User.GetUserId(); //get user id by token
        await userService.UpdateUserAsync(userId, mapper.Map<UserModel>(user),
            cancellationToken); //searching and updating process

        logger.LogInformation("User was updated");

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Get user");

        var userId = User.GetUserId(); //get user id by token
        var user = await userService.GetByIdAsync(userId, cancellationToken); //find user by id
        var viewModel = mapper.Map<UserViewModel>(user); // put user in userViewModel

        logger.LogInformation("Get user");

        return Ok(viewModel);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to delete user");

        await userService.DeleteUserAsync(User.GetUserId(), cancellationToken); //delete user by id

        logger.LogInformation("User was deleted");

        return Ok("User was deleted");
    }
}