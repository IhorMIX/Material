using AutoMapper;
using Material.BLL.Models;
using Material.BLL.Services.Interfaces;
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

        await materialService.CreateMaterialAsync(mapper.Map<MaterialEntityModel>(material), cancellationToken);

        logger.LogInformation("Material was created");

        return Ok();
    }
    
    [HttpGet("material-by-id")]
    public async Task<IActionResult> GetMaterialAsync([FromQuery] int materialId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get material");

        var material = await materialService.GetByIdAsync(materialId, cancellationToken);
        var viewModel = mapper.Map<MaterialViewModel>(material);

        logger.LogInformation("Get material");

        return Ok(viewModel);
    }

    [HttpDelete("{materialId:int}")]
    public async Task<IActionResult> DeleteMaterialAsync(int materialId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to delete material");

        await materialService.DeleteMaterialAsync(materialId, cancellationToken);

        logger.LogInformation("Material was deleted");

        return Ok("Material was deleted");
    }
}