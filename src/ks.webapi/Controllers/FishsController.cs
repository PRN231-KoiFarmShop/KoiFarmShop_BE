using ks.application.Models.Fish;
using ks.application.Models.Response;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ks.webapi.Controllers;
[ApiController]

[Route("api/[controller]")]
public class FishsController : ControllerBase
{
    private readonly IFishService fishService;
    public FishsController(IFishService fishService)
    {
        this.fishService = fishService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int? pageSize,
        [FromQuery] string search = "",
        [FromQuery] int pageIndex = 0)
    {
        var result = await fishService.GetAsync(search: search,
            pageSize: pageSize,
            pageIndex: pageIndex,
            cancellationToken: default);
        return result?.Count > 0
            ? Ok(result)
            : throw new Exception("List is empty");

    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] FishCreateModel model)
    {
        var result = await fishService.CreateFishAsync(model, default);
        return result is not null
            ? Ok(result)
            : throw new Exception("Created Failed");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id,
        [FromBody] FishUpdateModel model)
    {
        var result = await fishService.UpdateAsync(id, model, default);
        return result
            ? Ok("Update Successful")
            : throw new Exception("Update Failed");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await fishService.GetById(id, default);
        return result is not null
            ? Ok(result)
            : throw new Exception($"Not found Id: {id}");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Del([FromRoute] Guid id)
    {
        var result = await fishService.RemoveAsync(id, default);
        return result
            ? Ok("Delete Successful")
            : throw new Exception("Delete Failed");
    }
}