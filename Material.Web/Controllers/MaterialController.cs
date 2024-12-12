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
public class MaterialController(IMaterialService materialService, ILogger<MaterialController> logger, IMapper mapper)
    : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateMaterial([FromBody] MaterialCreateViewModel material,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to create material");
        var userId = User.GetUserId();
        
        await materialService.CreateMaterialAsync(userId, mapper.Map<MaterialEntityModel>(material), cancellationToken);
        logger.LogInformation("Material was created");

        return Ok();
    }
    
    [HttpGet("material-by-id")]
    public async Task<IActionResult> GetMaterial([FromQuery] int materialId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get material");
        
        var material = await materialService.GetByIdAsync(materialId, cancellationToken);
        var viewModel = mapper.Map<MaterialViewModel>(material);

        logger.LogInformation("Get material");

        return Ok(viewModel);
    }

    [HttpDelete("{materialId:int}")]
    public async Task<IActionResult> DeleteMaterial(int materialId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to delete material");
        var userId = User.GetUserId();
        await materialService.DeleteMaterialAsync(userId, materialId, cancellationToken);

        logger.LogInformation("Material was deleted");
        return Ok();
    }
    
    [HttpPost("add-to-favorite")]
    public async Task<IActionResult> AddToFavorite([FromBody] AddMaterialToFavoriteModel addMaterialToFavorite, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to add material");
        var userId = User.GetUserId();
        await materialService.AddMaterialToFavoritesAsync(userId,addMaterialToFavorite.MaterialId , cancellationToken);
        logger.LogInformation("Material was added");
        return Ok();
    }
    
    [HttpDelete("remove-from-favorite")]
    public async Task<IActionResult> RemoveFromFavorite([FromQuery] int materialId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to remove material");
        var userId = User.GetUserId();
        await materialService.RemoveMaterialFromFavoritesAsync(userId, materialId, cancellationToken);

        logger.LogInformation("Material was removed");

        return Ok("Material was deleted");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFavoriteList(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var materials = await materialService.GetMaterialsFromFavoriteListAsync(userId, cancellationToken);

        var materialViewModels = mapper.Map<IEnumerable<MaterialViewModel>>(materials);
        return Ok(materialViewModels);
    }

}