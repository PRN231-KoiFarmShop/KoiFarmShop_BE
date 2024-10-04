using ks.application.Models.Fish;
using ks.application.Models.FishPackages;
using ks.application.Models.Response;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers;
[ApiController]
[Route("api/fish-packages")]
public class FishPackagesController : ControllerBase
{
    private readonly IFishPackageService fishPackageService;
    public FishPackagesController(IFishPackageService fishPackageService)
    {
        this.fishPackageService = fishPackageService;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] FishPackageCreateModel model)
    {
        var result = await fishPackageService.CreateAsync(model, default);
        return result is not null
            ? Ok(result)
            : throw new Exception("Created Failed");
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? pageSize,
        int pageIndex = 0,
        string search = "")
    {
        var result = await fishPackageService.GetAsync(pageSize, search, pageIndex, default);
        return result?.Count > 0
            ? Ok(result)
            : throw new Exception("Empty");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await fishPackageService.GetByIdAsync(id, default);
        return result is not null
            ? Ok(result)
            : throw new Exception($"Not found FishPack with Id: {id}");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await fishPackageService.DeleteAsync(id, default);
        return result
            ? NoContent()
            :throw new Exception("Delete Failed");
    }
}